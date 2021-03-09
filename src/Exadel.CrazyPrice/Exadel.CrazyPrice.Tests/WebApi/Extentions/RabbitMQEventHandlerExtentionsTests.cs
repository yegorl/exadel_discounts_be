using Exadel.CrazyPrice.Services.Bus.EventBus;
using Exadel.CrazyPrice.Services.Bus.EventBus.Abstractions;
using Exadel.CrazyPrice.Services.Bus.EventBusRabbitMQ;
using Exadel.CrazyPrice.Services.Bus.IntegrationBus.IntegrationEvents;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.EventHandling;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Extentions
{
    public class RabbitMQEventHandlerExtentionsTests
    {
        private readonly IHost _host;

        public RabbitMQEventHandlerExtentionsTests()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    
                    services.AddTransient<IIntegrationEventService, IntegrationEventService>();
                    services.AddSingleton<IEventBus, EventBusRabbitMQ>();
                    services.AddSingleton<IRabbitMQPersistentConnection, DefaultRabbitMQPersistentConnection>();
                    services.AddSingleton<IConnectionFactory, ConnectionFactory>();
                    services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

                    services.AddEventBusHandlers(null);
                });
            });
            _host = builder.Build();
        }

        [Fact]
        public void AddEventBusHandlersTest()
        {
            var handlerUser = _host.Services.GetService<PromocodeAddedIntegrationEventHandler<UserMailContent>>();
            var handlerCompany = _host.Services.GetService<PromocodeAddedIntegrationEventHandler<CompanyMailContent>>();

            handlerUser.Should().NotBeNull();
            handlerCompany.Should().NotBeNull();
        }
    }
}
