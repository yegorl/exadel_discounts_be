using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Modules.EventBus;
using Exadel.CrazyPrice.Modules.EventBus.Abstractions;
using Exadel.CrazyPrice.Modules.EventBusRabbitMQ;
using IntegrationBus.IntegrationEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace IntegrationBus.Extentions
{
    public static class RabbitMQExtentions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IIntegrationEventService, IntegrationEventService>();

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory
                {
                    HostName = configuration["MessageBroker:EventBusConnection"],
                    DispatchConsumersAsync = true,

                    UserName = !string.IsNullOrEmpty(configuration["MessageBroker:EventBusUserName"]) ? configuration.GetString("MessageBroker:EventBusUserName")
                        .ToStringWithValue("MessageBroker:EventBusUserName", null, false) : null,

                    Password = !string.IsNullOrEmpty(configuration["MessageBroker:EventBusPassword"]) ? configuration.GetString("MessageBroker:EventBusPassword")
                        .ToStringWithValue("MessageBroker:EventBusPassword", null, false) : null
                };

                var retryCount = configuration.GetString("MessageBroker:EventBusRetryCount").ToInt(5, false);

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["MessageBroker:SubscriptionClientName"];
            var retryCount = configuration.GetString("MessageBroker:EventBusRetryCount").ToInt(5, false);

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp => new EventBusRabbitMQ(sp, subscriptionClientName, retryCount));

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}
