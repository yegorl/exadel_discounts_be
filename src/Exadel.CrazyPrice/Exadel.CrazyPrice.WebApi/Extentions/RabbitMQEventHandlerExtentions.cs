using Exadel.CrazyPrice.Services.Common.IntegrationEvents.EventHandling;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    public static class RabbitMQEventHandlerExtentions
    {
        public static IServiceCollection AddEventBusHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<PromocodeAddedIntegrationEventHandler<UserMailContent>>();
            services.AddTransient<PromocodeAddedIntegrationEventHandler<CompanyMailContent>>();
            return services;
        }
    }
}
