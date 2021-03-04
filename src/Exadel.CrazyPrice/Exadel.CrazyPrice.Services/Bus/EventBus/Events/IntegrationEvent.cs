using System;
using System.Text.Json.Serialization;

namespace Exadel.CrazyPrice.Services.Bus.EventBus.Events
{
    public record IntegrationEvent
    {
        public IntegrationEvent()
        {
            EventId = Guid.NewGuid();
            EventCreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent(string applicationName, BusParams busParams)
        {
            EventId = Guid.NewGuid();
            EventCreationDate = DateTime.UtcNow;

            ApplicationName = applicationName;
            BusParams = busParams;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid eventId, DateTime eventCreateDate, string applicationName, BusParams busParams)
        {
            EventId = eventId;
            EventCreationDate = eventCreateDate;
            ApplicationName = applicationName;
            BusParams = busParams;
        }

        public Guid EventId { get; private init; }

        public DateTime EventCreationDate { get; private init; }

        public string ApplicationName { get; private init; }

        public BusParams BusParams { get; private init; }
    }
}
