using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Events;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models;
using Exadel.CrazyPrice.Services.Mail.MailBuilder;
using Exadel.CrazyPrice.Services.Mail.MailBuilder.Models.Option;
using Exadel.CrazyPrice.Services.Mail.MailClient;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Extentions;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Helpers;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents.EventHandling
{
    public class PromocodeAddedUserMailIntegrationEventHandler :
        IIntegrationEventHandler<PromocodeAddedIntegrationEvent<UserMailContent>>
    {
        private readonly IMailClient _mailClient;
        private readonly IMailBuilder _mailBuilder;
        private readonly ILogger<PromocodeAddedUserMailIntegrationEventHandler> _logger;

        public PromocodeAddedUserMailIntegrationEventHandler(IMailClient mailClient, IMailBuilder mailBuilder, ILogger<PromocodeAddedUserMailIntegrationEventHandler> logger)
        {
            _mailClient = mailClient ?? throw new System.ArgumentNullException(nameof(mailClient));
            _mailBuilder = mailBuilder ?? throw new System.ArgumentNullException(nameof(mailBuilder));
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PromocodeAddedIntegrationEvent<UserMailContent> @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.EventId, ApplicationInfo.ApplicationName, @event);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Exadel Info", "temporarytestinguser@gmail.com"));
            message.To.Add(new MailboxAddress("Mr. Friend", "this.all@mail.ru"));
            message.Subject = "Exadel Promo";
            message.Body = _mailBuilder.GetMessageBody(MailBodyOption.User, LanguageOption.Ru, @event.Content);

            await _mailClient.SendAsync(message);

            await Task.CompletedTask;
        }
    }
}
