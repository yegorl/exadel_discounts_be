using Exadel.CrazyPrice.WebApi.IntegrationEvents.EventHandling;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    public static class RabbitMQEventHandlerExtentions
    {
        public static IServiceCollection AddEventBusHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddTransient<PromocodeAddedIntegrationEventHandler>();
        }
    }
}
