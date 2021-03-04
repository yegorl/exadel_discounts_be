using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using Exadel.CrazyPrice.Services.Bus.IntegrationBus.IntegrationEvents;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Services.Common.IntegrationEvents.EventHandling
{
    public class PromocodeAddedIntegrationEventHandler<TContent> :
        IIntegrationEventHandler<IntegrationEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<PromocodeAddedIntegrationEventHandler<TContent>> _logger;

        public PromocodeAddedIntegrationEventHandler(IIntegrationEventService integrationEventService,
            ILogger<PromocodeAddedIntegrationEventHandler<TContent>> logger)
        {
            _integrationEventService = integrationEventService;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(IntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.EventId, @event.ApplicationName, @event);

            await _integrationEventService.PublishThroughEventBusAsync(@event);
        }
    }
}
