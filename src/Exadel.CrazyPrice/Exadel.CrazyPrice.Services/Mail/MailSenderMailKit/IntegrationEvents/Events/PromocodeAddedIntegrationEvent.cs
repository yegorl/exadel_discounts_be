using Exadel.CrazyPrice.Modules.EventBus.Events;
using System;

namespace MailSenderMailKit.IntegrationEvents.Events
{
    public record PromocodeAddedIntegrationEvent(
        Guid DiscountId,
        string DiscountName,
        string CompanyMail,
        string UserName,
        string UserMail,
        string PromocodeValue) : IntegrationEvent;
}
