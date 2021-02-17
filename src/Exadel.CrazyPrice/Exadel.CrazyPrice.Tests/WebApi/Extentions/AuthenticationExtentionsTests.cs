using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Exadel.CrazyPrice.WebApi.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Extentions
{
    public class AuthenticationExtentionsTests
    {
        private readonly IServiceCollection _service = new ServiceCollection();

        public AuthenticationExtentionsTests()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[]
                {
                    new("Auth:PolicyName", "PolicyName"),
                    new("Auth:Origins:0","OAuthAppName")
                })
                .Build();
            _service.AddScoped(_ => configuration);
            _service.TryAddSingleton<IWebApiConfiguration, WebApiConfiguration>();
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
            act.Should().NotThrow();
        }
    }
}
