using System.Threading.Tasks;
using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Mail.MailClient;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Helpers;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents.EventHandling
{
    public class PromocodeAddedMailIntegrationEventHandler :
        IIntegrationEventHandler<PromocodeAddedIntegrationEvent>
    {
        private readonly IMailClient _mailClient;
        private readonly ILogger<PromocodeAddedMailIntegrationEventHandler> _logger;

        public PromocodeAddedMailIntegrationEventHandler(IMailClient mailClient, ILogger<PromocodeAddedMailIntegrationEventHandler> logger)
        {
            _mailClient = mailClient ?? throw new System.ArgumentNullException(nameof(mailClient));
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PromocodeAddedIntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, ApplicationInfo.ApplicationName, @event);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Exadel Info", "temporarytestinguser@gmail.com"));
            message.To.Add(new MailboxAddress("Mr. Friend", "this.all@mail.ru"));
            message.Subject = "Exadel Promo";

            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = $"Hello, {@event.UserName}. Service Name: {@event.DiscountName}. Promocode Number: {@event.PromocodeValue}."
            };

            await _mailClient.SendAsync(message);

            await Task.CompletedTask;
        }
    }
}
