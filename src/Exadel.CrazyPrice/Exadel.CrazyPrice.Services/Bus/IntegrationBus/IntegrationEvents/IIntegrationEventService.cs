using Exadel.CrazyPrice.Modules.EventBus.Events;
using System.Threading.Tasks;

namespace IntegrationBus.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt, string appName);
    }
}
