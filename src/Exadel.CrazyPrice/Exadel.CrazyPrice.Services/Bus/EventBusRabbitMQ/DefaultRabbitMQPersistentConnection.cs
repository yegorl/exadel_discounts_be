using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;

namespace Exadel.CrazyPrice.Services.Bus.EventBusRabbitMQ
{
    public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<DefaultRabbitMQPersistentConnection> _logger;
        private readonly int _retryCount;
        private readonly object _syncRoot = new();
        private IConnection _connection;
        private bool _disposed;

        public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogger<DefaultRabbitMQPersistentConnection> logger, int retryCount = 5)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _retryCount = retryCount;
        }

        public bool IsConnected =>
            _connection != null && _connection.IsOpen && !_disposed;

        public IModel CreateModel() =>
            IsConnected
                ? _connection.CreateModel()
                : throw new InvalidOperationException("No RabbitMQ connections are available to perform this action.");

        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ Client is trying to connect...");

            lock (_syncRoot)
            {
                var policy = RetryPolicy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                        _logger.LogWarning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                    }
                );

                policy.Execute(() =>
                {
                    _connection = _connectionFactory
                          .CreateConnection();
                });

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;

                    _logger.LogInformation("RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events.", _connection.Endpoint.HostName);

                    return true;
                }

                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened.");

                return false;
            }
        }


        #region Reconnect when failure.
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e) =>
            LogAndTryConnect("A RabbitMQ connection is blocked. Trying to re-connect...");

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e) =>
            LogAndTryConnect("A RabbitMQ connection throw exception. Trying to re-connect...");

        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason) =>
            LogAndTryConnect("A RabbitMQ connection is on shutdown. Trying to re-connect...");

        private void LogAndTryConnect(string message)
        {
            if (_disposed)
            {
                return;
            }

            _logger.LogWarning(message);

            TryConnect();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }
        #endregion
    }
}
