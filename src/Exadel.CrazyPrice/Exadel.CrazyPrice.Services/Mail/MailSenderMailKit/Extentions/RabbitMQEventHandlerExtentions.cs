using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Mail.MailClient;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Configuration;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents.EventHandling;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents.Events;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Extentions
{
    public static class RabbitMQEventHandlerExtentions
    {
        public static IServiceCollection AddEventBusServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpConfiguration>(configuration.GetSection("Smtp"));
            services.TryAddSingleton<IValidateOptions<SmtpConfiguration>, SmtpConfigurationValidation>();

            services.AddTransient<IMailClient, CrazyPriceMailClient>();
            services.AddTransient<PromocodeAddedMailIntegrationEventHandler>();

            return services;
        }

        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<PromocodeAddedIntegrationEvent, PromocodeAddedMailIntegrationEventHandler>();

            return app;
        }
    }
}
