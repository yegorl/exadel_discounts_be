using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Services.Bus.IntegrationBus.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
