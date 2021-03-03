using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Events;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models;
using Exadel.CrazyPrice.Services.Mail.MailClient;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Configuration;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.IntegrationEvents.EventHandling;
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

            services.AddTransient<PromocodeAddedUserMailIntegrationEventHandler>();
            services.AddTransient<PromocodeAddedCompanyMailIntegrationEventHandler>();

            services.AddTransient<BusParams<PromocodeAddedIntegrationEvent<UserMailContent>>, UserBusParams>();
            services.AddTransient<BusParams<PromocodeAddedIntegrationEvent<CompanyMailContent>>, CompanyBusParams>();

            services.AddTransient<UserBusParams>();
            services.AddTransient<CompanyBusParams>();

            return services;
        }

        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<PromocodeAddedIntegrationEvent<UserMailContent>, PromocodeAddedUserMailIntegrationEventHandler, UserBusParams>();
            eventBus.Subscribe<PromocodeAddedIntegrationEvent<CompanyMailContent>, PromocodeAddedCompanyMailIntegrationEventHandler, CompanyBusParams>();

            return app;
        }
    }
}
