using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Events;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents
{
    public record UserBusParams : BusParams<PromocodeAddedIntegrationEvent<UserMailContent>>
    {
        public UserBusParams() : base("crazyprice.direct", "mail.user")
        { }
    }
}