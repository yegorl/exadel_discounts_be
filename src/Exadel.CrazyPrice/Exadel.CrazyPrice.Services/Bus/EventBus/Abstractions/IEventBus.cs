using Exadel.CrazyPrice.Services.Bus.EventBus.Events;

namespace Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent evt);

        void Subscribe<T, TH, TP>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
            where TP : BusParams<T>;

        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }
}
