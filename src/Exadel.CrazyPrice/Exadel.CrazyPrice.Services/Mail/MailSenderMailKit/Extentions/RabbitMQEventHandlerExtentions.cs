using Exadel.CrazyPrice.Modules.EventBus.Abstractions;
using MailSenderMailKit.IntegrationEvents.EventHandling;
using MailSenderMailKit.IntegrationEvents.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailSenderMailKit.Extentions
{
    public static class RabbitMQEventHandlerExtentions
    {
        public static IServiceCollection AddEventBusHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddTransient<PromocodeAddedMailIntegrationEventHandler>();
        }

        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<PromocodeAddedIntegrationEvent, PromocodeAddedMailIntegrationEventHandler>();

            return app;
        }
    }
}
