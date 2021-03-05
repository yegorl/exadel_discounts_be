using Exadel.CrazyPrice.Services.Bus.EventBus.Events;

namespace Exadel.CrazyPrice.Services.Common.IntegrationEvents.Events
{
    public record PromocodeAddedIntegrationEvent<T>(
        T Content, 
        string ApplicationName,
        BusParams BusParams) : IntegrationEvent(ApplicationName, BusParams);
}
