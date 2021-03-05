using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions
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
