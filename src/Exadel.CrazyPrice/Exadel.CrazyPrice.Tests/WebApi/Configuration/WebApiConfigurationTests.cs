using Exadel.CrazyPrice.WebApi.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Configuration
{
    public class WebApiConfigurationTests
    {
        [Fact]
        public void WebApiConfigurationTest()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[]
                {
                    new("Auth:Origins:0", "\"https://localhost\""),
                    new("Auth:IssuerUrl","IssuerUrl"),
                    new("Auth:ApiName","ApiName"),
                    new("Auth:ApiSecret","ApiSecret"),
                    new("Auth:PolicyName","PolicyName"),
                    new("Auth:Swagger:AuthorizationUrl", "http://localhost"),
                    new("Auth:Swagger:TokenUrl","http://localhost"),
                    new("Auth:Swagger:RefreshUrl","http://localhost"),
                    new("Auth:Swagger:Scopes:Scope1","Scope1"),
                })
                .Build();

            var apiConfig = new WebApiConfiguration(config);

            var origins = apiConfig.Origins;
            origins.Should().NotBeNull();

            var issuerUrl = apiConfig.IssuerUrl;
            issuerUrl.Should().NotBeNull();

            var apiName = apiConfig.ApiName;
            apiName.Should().NotBeNull();

            var apiSecret = apiConfig.ApiSecret;
            apiSecret.Should().NotBeNull();

            var authorizationUrl = apiConfig.AuthorizationUrl;
            authorizationUrl.Should().NotBeNull();

            var tokenUrl = apiConfig.TokenUrl;
            tokenUrl.Should().NotBeNull();

            var refreshUrl = apiConfig.RefreshUrl;
            refreshUrl.Should().NotBeNull();

            var scopes = apiConfig.Scopes;
            scopes.Should().NotBeNull();
        }
    }
}
