using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Events;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents
{
    public record CompanyBusParams : BusParams<PromocodeAddedIntegrationEvent<CompanyMailContent>>
    {
        public CompanyBusParams() : base("crazyprice.direct", "mail.company")
        { }
    }
}
