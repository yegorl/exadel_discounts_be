using Exadel.CrazyPrice.Modules.EventBus.Events;
using System;

namespace Exadel.CrazyPrice.WebApi.IntegrationEvents.Events
{
    public record PromocodeAddedIntegrationEvent(
        Guid DiscountId,
        string DiscountName,
        string CompanyMail,
        string UserName,
        string UserMail,
        string PromocodeValue) : IntegrationEvent;
}
