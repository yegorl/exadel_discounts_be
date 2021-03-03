﻿using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Events;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models;
using Exadel.CrazyPrice.Services.Mail.MailClient;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Helpers;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents.EventHandling
{
    public class PromocodeAddedCompanyMailIntegrationEventHandler :
        IIntegrationEventHandler<PromocodeAddedIntegrationEvent<CompanyMailContent>>
    {
        private readonly IMailClient _mailClient;
        private readonly ILogger<PromocodeAddedCompanyMailIntegrationEventHandler> _logger;

        public PromocodeAddedCompanyMailIntegrationEventHandler(IMailClient mailClient, ILogger<PromocodeAddedCompanyMailIntegrationEventHandler> logger)
        {
            _mailClient = mailClient ?? throw new System.ArgumentNullException(nameof(mailClient));
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PromocodeAddedIntegrationEvent<CompanyMailContent> @event)
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.EventId, ApplicationInfo.ApplicationName, @event);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Exadel Info", "temporarytestinguser@gmail.com"));
            message.To.Add(new MailboxAddress("Mr. Friend", "this.all@mail.ru"));
            message.Subject = "Exadel Promo";

            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = $"Hello, {@event.Content.Company.Name}. Service Name: {@event.Content.DiscountName}. Promocode Number: {@event.Content.PromocodeValue}."
            };

            await _mailClient.SendAsync(message);

            await Task.CompletedTask;
        }
    }
}
