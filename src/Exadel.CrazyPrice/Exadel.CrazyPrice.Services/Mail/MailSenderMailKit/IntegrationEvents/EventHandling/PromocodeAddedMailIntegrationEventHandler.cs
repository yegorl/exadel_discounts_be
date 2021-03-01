using Exadel.CrazyPrice.Modules.EventBus.Abstractions;
using IntegrationBus.IntegrationEvents;
using MailSenderMailKit.Helpers;
using MailSenderMailKit.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MailSenderMailKit.IntegrationEvents.EventHandling
{
    public class PromocodeAddedMailIntegrationEventHandler :
        IIntegrationEventHandler<PromocodeAddedIntegrationEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<PromocodeAddedMailIntegrationEventHandler> _logger;

        public PromocodeAddedMailIntegrationEventHandler(IIntegrationEventService integrationEventService,
            ILogger<PromocodeAddedMailIntegrationEventHandler> logger)
        {
            _integrationEventService = integrationEventService;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PromocodeAddedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, ApplicationInfo.ApplicationName, @event);

            await Task.CompletedTask;
        }
    }
}
