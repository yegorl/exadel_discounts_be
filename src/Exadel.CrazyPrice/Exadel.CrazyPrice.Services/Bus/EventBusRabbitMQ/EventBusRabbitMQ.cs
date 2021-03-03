using Exadel.CrazyPrice.Services.Bus.EventBus;
using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using Exadel.CrazyPrice.Services.Bus.EventBus.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using InMemoryEventBusSubscriptionsManager = Exadel.CrazyPrice.Services.Bus.EventBus.InMemoryEventBusSubscriptionsManager;

namespace Exadel.CrazyPrice.Services.Bus.EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        //private const string BROKER_NAME = "crazyprice_event_bus";

        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly int _retryCount;

        //private IModel _consumerChannel;
        private readonly Dictionary<string, IModel> _consumerChannels;
        private readonly IServiceProvider _serviceProvider;
        //private string _queueName;

        public EventBusRabbitMQ(IServiceProvider serviceProvider, int retryCount = 5)
        {
            _serviceProvider = serviceProvider;

            var logger = _serviceProvider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            var persistentConnection = _serviceProvider.GetRequiredService<IRabbitMQPersistentConnection>();
            var subsManager = _serviceProvider.GetRequiredService<IEventBusSubscriptionsManager>();

            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            //_consumerChannel = CreateConsumerChannel();
            _consumerChannels = new Dictionary<string, IModel>();
            _retryCount = retryCount;
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        public void Publish(IntegrationEvent evt)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", evt.EventId, $"{time.TotalSeconds:n1}", ex.Message);
                });

            var eventName = evt.GetNormalizeTypeName();

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", evt.EventId, eventName);
            _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", evt.EventId);
            using var channel = GetChannelWithDeclare(evt.BusParams);

            channel.QueueBind(evt.BusParams.QueueName, evt.BusParams.ExchangeName, eventName);

            var message = JsonSerializer.Serialize(evt, evt.GetType(), new JsonSerializerOptions
            {
                WriteIndented = false,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)

            });
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent

                _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", evt.EventId);

                channel.BasicPublish(
                    exchange: evt.BusParams.ExchangeName,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            });
        }

        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).GetNormalizeTypeName());

            DoInternalSubscription(eventName);
            _subsManager.AddDynamicSubscription<TH>(eventName);
            StartBasicConsume(eventName);
        }

        public void Subscribe<T, TH, TP>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
            where TP : BusParams<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetNormalizeTypeName());

            _subsManager.AddSubscription<T, TH, TP>();

            DoInternalSubscription(eventName);

            var busParams = _serviceProvider.GetService<TP>();
            var key = GetBusParamsKey(busParams);

            if (!_consumerChannels.ContainsKey(key))
            {
                _consumerChannels.Add(key, CreateConsumerChannel(eventName));
            }
            StartBasicConsume(eventName);
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            _subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        private IModel GetChannelWithDeclare(BusParams busParams)
        {
            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: busParams.ExchangeName, type: ExchangeType.Direct, durable: true, autoDelete: false);
            channel.QueueDeclare(queue: busParams.QueueName, durable: true, exclusive: false, autoDelete: false);

            return channel;
        }

        private IModel CreateConsumerChannel(string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _logger.LogTrace("Creating RabbitMQ consumer channel.");

            var busParams = GetBusParams(eventName);
            var channel = GetChannelWithDeclare(busParams);

            channel.CallbackException += (sender, ea) =>
            {
                _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel.");
                var key = GetBusParamsKey(busParams);

                _consumerChannels[key].Dispose();
                _consumerChannels[key] = CreateConsumerChannel(eventName);
                StartBasicConsume(eventName);
            };

            return channel;
        }

        private void StartBasicConsume(string eventName)
        {
            _logger.LogTrace("Starting RabbitMQ basic consume");

            var busParams = GetBusParams(eventName);

            var consumerChannel = _consumerChannels[GetBusParamsKey(busParams)];
            if (consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(consumerChannel);

                consumer.Received += Consumer_Received;

                consumerChannel.BasicConsume(
                    queue: busParams.QueueName,
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);

            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake exception requested: \"{message}\"");
                }

                await ProcessEvent(eventName, message);
                _consumerChannels[GetBusParamsKey(GetBusParams(eventName))].BasicAck(eventArgs.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "ERROR Processing message \"{Message}\"", message);
            }

            // Even on exception we take the message off the queue.
            // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
            // For more information see: https://www.rabbitmq.com/dlx.html
            //_consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);

            await Task.CompletedTask;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    if (subscription.IsDynamic)
                    {
                        var handler = subscription.HandlerType as IDynamicIntegrationEventHandler;
                        if (handler == null)
                        {
                            continue;
                        }

                        var eventType = _subsManager.GetEventTypeByName(eventName);
                        dynamic eventData = JsonSerializer.Deserialize(message, eventType);

                        await Task.Yield();
                        await handler.Handle(eventData);
                    }
                    else
                    {
                        var handler = subscription.HandlerType;
                        if (handler == null)
                        {
                            continue;
                        }

                        var handlerConstructor = handler.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                            .OrderByDescending(c => c.GetParameters().Length).First();

                        var parameters = handlerConstructor.GetParameters();
                        var arguments = parameters.Select(p => _serviceProvider.GetService(p.ParameterType)).ToArray();

                        dynamic objectHandler = handlerConstructor.Invoke(arguments);

                        var eventType = _subsManager.GetEventTypeByName(eventName);
                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                        dynamic integrationEvent = JsonSerializer.Deserialize(message, eventType);

                        await Task.Yield();
                        await (Task)concreteType.GetMethod("Handle").Invoke(objectHandler, new object[] { integrationEvent });
                    }
                }
            }
            else
            {
                _logger.LogWarning("No subscription for RabbitMQ event: {EventName}.", eventName);
            }
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            using var channel = _persistentConnection.CreateModel();
            var busParams = GetBusParams(eventName);
            channel.QueueUnbind(queue: busParams.QueueName,
                exchange: busParams.ExchangeName,
                routingKey: eventName);

            if (!_subsManager.IsEmpty)
            {
                return;
            }

            _consumerChannels[GetBusParamsKey(busParams)].Close();
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            if (containsKey)
            {
                return;
            }

            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var busParams = GetBusParams(eventName);

            using var channel = _persistentConnection.CreateModel();
            channel.QueueBind(queue: busParams!.QueueName,
                exchange: busParams.ExchangeName,
                routingKey: eventName);
        }

        private BusParams GetBusParams(string eventName) =>
            (BusParams)_serviceProvider.GetService(_subsManager.GetTypeParams(eventName));

        private string GetBusParamsKey(BusParams busParams) =>
            $"{busParams.ExchangeName}.{busParams.QueueName}";

        #region Dispose
        public void Dispose()
        {

            foreach (var (key, model) in _consumerChannels)
            {
                model?.Dispose();
            }

            _subsManager.Clear();
        }
        #endregion
    }
}
