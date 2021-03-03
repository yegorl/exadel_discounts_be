using RabbitMQ.Client;
using System;

namespace Exadel.CrazyPrice.Services.Bus.EventBusRabbitMQ
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
