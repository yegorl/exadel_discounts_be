using Exadel.CrazyPrice.Modules.EventBus.Abstractions;
using Exadel.CrazyPrice.WebApi.Helpers;
using Exadel.CrazyPrice.WebApi.IntegrationEvents.Events;
using IntegrationBus.IntegrationEvents;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.WebApi.IntegrationEvents.EventHandling
{
    public class PromocodeAddedIntegrationEventHandler :
        IIntegrationEventHandler<PromocodeAddedIntegrationEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<PromocodeAddedIntegrationEventHandler> _logger;

        public PromocodeAddedIntegrationEventHandler(IIntegrationEventService integrationEventService,
            ILogger<PromocodeAddedIntegrationEventHandler> logger)
        {
            _integrationEventService = integrationEventService;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PromocodeAddedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, ApplicationInfo.ApplicationName, @event);

            await _integrationEventService.PublishThroughEventBusAsync(@event, ApplicationInfo.ApplicationName);
        }
    }
}
