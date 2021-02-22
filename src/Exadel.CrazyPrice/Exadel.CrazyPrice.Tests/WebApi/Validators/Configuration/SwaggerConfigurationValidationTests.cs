using Exadel.CrazyPrice.WebApi.Configuration;
using Exadel.CrazyPrice.WebApi.Validators.Configuration;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Validators.Configuration
{
    public class SwaggerConfigurationValidationTests
    {
        [Fact]
        public void SwaggerConfigurationValidationSucceededTest()
        {
            var config = new SwaggerConfiguration()
            {
                ApiName = "ApiName",
                OAuthAppName = "OAuthAppName",
                OAuthClientId = "OAuthClientId",
                AuthorizationUrl = "http://localhost",
                TokenUrl = "http://localhost",
                RefreshUrl = "http://localhost",
                Scopes = new Dictionary<string, string> { { "key", "value" } }
            };

            var rule = new SwaggerConfigurationValidation();
            rule.Validate(null, config).Succeeded.Should().BeTrue();
        }

        [Fact]
        public void SwaggerConfigurationValidationFailTest()
        {
            var config = new SwaggerConfiguration()
            {
                ApiName = "ApiName",
                OAuthAppName = "OAuthAppName",
                OAuthClientId = "OAuthClientId",
                AuthorizationUrl = "http://localhost",
                TokenUrl = "http://localhost",
                RefreshUrl = "http://localhost",
                Scopes = new Dictionary<string, string>()
            };

            var rule = new SwaggerConfigurationValidation();
            rule.Validate(null, config).Failed.Should().BeTrue();
        }

        [Theory]
        [InlineData("", "OAuthAppName", "OAuthClientId", "http://localhost", "http://localhost", "http://localhost")]
        [InlineData(null, "OAuthAppName", "OAuthClientId", "http://localhost", "http://localhost", "http://localhost")]
        [InlineData("ApiName", "", "OAuthClientId", "http://localhost", "http://localhost", "http://localhost")]
        [InlineData("ApiName", null, "OAuthClientId", "http://localhost", "http://localhost", "http://localhost")]
        [InlineData("ApiName", "OAuthAppName", "", "http://localhost", "http://localhost", "http://localhost")]
        [InlineData("ApiName", "OAuthAppName", null, "http://localhost", "http://localhost", "http://localhost")]

        [InlineData("ApiName", "OAuthAppName", "OAuthClientId", "http://localhost/", "http://localhost", "http://localhost")]
        [InlineData("ApiName", "OAuthAppName", "OAuthClientId", "", "http://localhost", "http://localhost")]
        [InlineData("ApiName", "OAuthAppName", "OAuthClientId", null, "http://localhost", "http://localhost")]

        [InlineData("ApiName", "OAuthAppName", "OAuthClientId", "http://localhost", "http://localhost/", "http://localhost")]
        [InlineData("ApiName", "OAuthAppName", "OAuthClientId", "http://localhost", "", "http://localhost")]
        [InlineData("ApiName", "OAuthAppName", "OAuthClientId", "http://localhost", null, "http://localhost")]

        [InlineData("ApiName", "OAuthAppName", "OAuthClientId", "http://localhost", "http://localhost", "http://localhost/")]
        [InlineData("ApiName", "OAuthAppName", "OAuthClientId", "http://localhost", "http://localhost", "")]
        [InlineData("ApiName", "OAuthAppName", "OAuthClientId", "http://localhost", "http://localhost", null)]
        public void SwaggerConfigurationValidationManyFailTest(
            string apiName,
            string oAuthAppName,
            string oAuthClientId,
            string authorizationUrl,
            string tokenUrl,
            string refreshUrl
        )
        {
            var config = new SwaggerConfiguration()
            {
                ApiName = apiName,
                OAuthAppName = oAuthAppName,
                OAuthClientId = oAuthClientId,
                AuthorizationUrl = authorizationUrl,
                TokenUrl = tokenUrl,
                RefreshUrl = refreshUrl,
                Scopes = new Dictionary<string, string>()
            };

            var rule = new SwaggerConfigurationValidation();
            rule.Validate(null, config).Failed.Should().BeTrue();
        }
    }
}
