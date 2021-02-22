using Exadel.CrazyPrice.WebApi.Configuration;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using IdentityModel.Client;
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
using System.Text.Json;
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
                new("Authorization:Origins:0", "OAuthAppName"),
                new("Authorization:IntrospectionDiscoveryPolicy:RequireHttps", "true"),
                new("Authorization:IntrospectionDiscoveryPolicy:RequireKeySet", "true"),
                new("Authorization:IntrospectionDiscoveryPolicy:AllowHttpOnLoopback", "true"),
                new("Authorization:IntrospectionDiscoveryPolicy:ValidateIssuerName", "true"),
                new("Authorization:IntrospectionDiscoveryPolicy:ValidateEndpoints", "true"),
                new("Authorization:IntrospectionDiscoveryPolicy:Authority", "https://localhost:44301"),
                new("Authorization:IntrospectionDiscoveryPolicy:AdditionalEndpointBaseAddresses:0", "https://localhost:44301")
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

        [Fact]
        public void SerializeSettings()
        {
            var introspectionDiscoveryPolicy = new DiscoveryPolicy
            {
                ValidateEndpoints = true,
                RequireHttps = true,
                ValidateIssuerName = true,
                AllowHttpOnLoopback = true,
                RequireKeySet = true,
                Authority = "https://identityserver:443",
                AdditionalEndpointBaseAddresses = { "https://identityserver", "https://identityserver:443" },
                //EndpointValidationExcludeList = { "https://lclhst", "https://lclhst:443" }
            };
            var value = JsonSerializer.Serialize(introspectionDiscoveryPolicy);
            value.Should().NotBeEmpty();
        }
    }
}
