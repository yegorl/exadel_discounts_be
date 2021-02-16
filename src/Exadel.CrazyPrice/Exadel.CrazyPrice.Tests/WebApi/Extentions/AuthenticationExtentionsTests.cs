using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Extentions
{
    public class AuthenticationExtentionsTests
    {
        private readonly IServiceCollection _service = new ServiceCollection();

        public AuthenticationExtentionsTests()
        {
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _service.AddScoped(_ => configuration);
            _service.AddCrazyPriceAuthentication();
        }

        [Fact]
        public void AddCrazyPriceAuthenticationTest()
        {
            var serviceName = "AuthenticationService";
            var hasService = _service.Any(serviceDescriptor => serviceDescriptor?.ImplementationType?.Name == serviceName);

            hasService.Should().BeTrue($"ServiceCollection is not contains {serviceName}.");
        }

        [Fact]
        public void UseCrazyPriceAuthenticationTest()
        {
            var appBuilder = new ApplicationBuilder(_service.BuildServiceProvider());
            Action act = () => appBuilder.UseCrazyPriceAuthentication().Build();

            // The CorsMiddleware exists.
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("A suitable constructor for type 'Microsoft.AspNetCore.Cors.Infrastructure.CorsMiddleware' could not be located. " +
                             "Ensure the type is concrete and services are registered for all parameters of a public constructor.");
        }
    }
}
