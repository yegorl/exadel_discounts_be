using System;
using Exadel.CrazyPrice.Services.Bus.EventBus.Events;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents.Events
{
    public record PromocodeAddedIntegrationEvent(
        Guid DiscountId,
        string DiscountName,
        string CompanyMail,
        string UserName,
        string UserMail,
        string PromocodeValue) : IntegrationEvent;
}
