using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Services.Bus.IntegrationBus.IntegrationEvents
{
    public class IntegrationEventService : IIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<IntegrationEventService> _logger;

        public IntegrationEventService(ILogger<IntegrationEventService> logger, IEventBus eventBus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt, string appName)
        {
            try
            {
                _logger.LogInformation("Publishing integration event: {IntegrationEventId_published} from {AppName} - ({@IntegrationEvent})", evt.Id, appName, evt);

                _eventBus.Publish(evt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", evt.Id, appName, evt);
            }

            await Task.CompletedTask;
        }
    }
}
