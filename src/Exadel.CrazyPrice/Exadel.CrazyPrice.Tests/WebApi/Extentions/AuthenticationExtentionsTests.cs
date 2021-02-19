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
            var keyValuePair = new KeyValuePair<string, string>[]
            {
                new("Authorization:ApiName", "ApiName"),
                new("Authorization:ApiSecret", "ApiSecret"),
                new("Authorization:IssuerUrl", "https://localhost:44301"),
                new("Authorization:PolicyName", "PolicyName"),
                new("Authorization:Origins:0", "OAuthAppName")
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(keyValuePair)
                .Build();

            var builder = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(configBuilder =>
                {
                    configBuilder
                        .AddInMemoryCollection(keyValuePair)
                        .Build();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.TryAddSingleton<AuthorizationConfiguration, AuthorizationConfiguration>();
                        services.AddCrazyPriceAuthentication(configuration);
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
