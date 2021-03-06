﻿using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Services.Bus.EventBus;
using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Bus.EventBusRabbitMQ;
using Exadel.CrazyPrice.Services.Bus.IntegrationBus.IntegrationEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using InMemoryEventBusSubscriptionsManager = Exadel.CrazyPrice.Services.Bus.EventBus.InMemoryEventBusSubscriptionsManager;

namespace Exadel.CrazyPrice.Services.Bus.IntegrationBus.Extentions
{
    public static class RabbitMQExtentions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IIntegrationEventService, IntegrationEventService>();

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory
                {
                    HostName = configuration["MessageBroker:EventBusConnection"],
                    DispatchConsumersAsync = true,

                    UserName = !string.IsNullOrEmpty(configuration["MessageBroker:EventBusUserName"]) ? configuration.GetString("MessageBroker:EventBusUserName")
                        .ToStringWithValue("MessageBroker:EventBusUserName", null, false) : null,

                    Password = !string.IsNullOrEmpty(configuration["MessageBroker:EventBusPassword"]) ? configuration.GetString("MessageBroker:EventBusPassword")
                        .ToStringWithValue("MessageBroker:EventBusPassword", null, false) : null
                };

                var retryCount = configuration.GetString("MessageBroker:EventBusRetryCount").ToInt(5, false);

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var retryCount = configuration.GetString("MessageBroker:EventBusRetryCount").ToInt(5, false);

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp => new EventBusRabbitMQ.EventBusRabbitMQ(sp, retryCount));

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}
