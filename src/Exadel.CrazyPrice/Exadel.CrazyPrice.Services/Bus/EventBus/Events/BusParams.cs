namespace Exadel.CrazyPrice.Services.Bus.EventBus.Events
{
    public record BusParams(string ExchangeName, string QueueName);

    public record BusParams<T>(string ExchangeName, string QueueName)
        : BusParams(ExchangeName, QueueName) where T : IntegrationEvent
    {
    }
}