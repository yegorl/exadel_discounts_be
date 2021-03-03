using System;
using System.Text.Json.Serialization;

namespace Exadel.CrazyPrice.Services.Bus.EventBus.Events
{
    public record IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        public Guid Id { get; private init; }

        public DateTime CreationDate { get; private init; }
    }
}
