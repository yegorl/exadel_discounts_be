using Exadel.CrazyPrice.Modules.EventBus.Events;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Modules.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
