using Exadel.CrazyPrice.WebApi.Configuration;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Extentions
{
    public class AuthenticationExtentionsTests
    {
        private readonly IHost _host;

        public AuthenticationExtentionsTests()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(configBuilder =>
                {
                    configBuilder
                        .AddInMemoryCollection(new KeyValuePair<string, string>[]
                        {
                            new("Auth:PolicyName", "PolicyName"),
                            new("Auth:Origins:0","OAuthAppName")
                        })
                        .Build();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.TryAddSingleton<IWebApiConfiguration, WebApiConfiguration>();
                        services.AddCrazyPriceAuthentication();
                    });
                });
            _host = builder.Build();
        }

        [Fact]
        public void AddCrazyPriceAuthenticationTest()
        {
            var hasCors = _host.Services.GetService<ICorsService>();
            var hasAuthentication = _host.Services.GetService<IAuthenticationService>();
            var hasIdentityServerAuthentication = _host.Services.GetService<IdentityServerAuthenticationHandler>();

            hasCors.Should().NotBeNull();
            hasAuthentication.Should().NotBeNull();
            hasIdentityServerAuthentication.Should().NotBeNull();
        }

        [Fact]
        public void UseCrazyPriceAuthenticationTest()
        {
            var appBuilder = new ApplicationBuilder(_host.Services);
            Action act = () => appBuilder.UseCrazyPriceAuthentication().Build();

            act.Should().NotThrow();
        }
    }
}
