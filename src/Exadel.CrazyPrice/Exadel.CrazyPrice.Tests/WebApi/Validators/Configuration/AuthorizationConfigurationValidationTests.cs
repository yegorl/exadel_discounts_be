using Exadel.CrazyPrice.WebApi.Configuration;
using Exadel.CrazyPrice.WebApi.Validators.Configuration;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Validators.Configuration
{
    public class AuthorizationConfigurationValidationTests
    {
        [Fact]
        public void AuthorizationConfigurationValidationSucceededTest()
        {
            var config = new AuthorizationConfiguration()
            {
                PolicyName = "PolicyName",
                ApiName = "ApiName",
                ApiSecret = "ApiSecret",
                IssuerUrl = "IssuerUrl",
                Origins = new[] { "http://localhost" }
            };

            var rule = new AuthorizationConfigurationValidation();
            rule.Validate(null, config).Succeeded.Should().BeTrue();
        }

        [Fact]
        public void AuthorizationConfigurationValidationFailTest()
        {
            var config = new AuthorizationConfiguration()
            {
                PolicyName = "PolicyName",
                ApiName = "ApiName",
                ApiSecret = "ApiSecret",
                IssuerUrl = "IssuerUrl",
                Origins = new[] { "" }
            };

            var rule = new AuthorizationConfigurationValidation();
            rule.Validate(null, config).Failed.Should().BeTrue();
        }

        [Theory]
        [InlineData("", "ApiName", "ApiSecret", "IssuerUrl")]
        [InlineData(null, "ApiName", "ApiSecret", "IssuerUrl")]
        [InlineData("PolicyName", "", "ApiSecret", "IssuerUrl")]
        [InlineData("PolicyName", null, "ApiSecret", "IssuerUrl")]
        [InlineData("PolicyName", "ApiName", "", "IssuerUrl")]
        [InlineData("PolicyName", "ApiName", null, "IssuerUrl")]
        [InlineData("PolicyName", "ApiName", "ApiSecret", "")]
        [InlineData("PolicyName", "ApiName", "ApiSecret", null)]
        public void AuthorizationConfigurationValidationManyFailTest(
            string policyName,
            string apiName,
            string apiSecret,
            string issuerUrl)
        {
            var config = new AuthorizationConfiguration()
            {
                PolicyName = policyName,
                ApiName = apiName,
                ApiSecret = apiSecret,
                IssuerUrl = issuerUrl,
                Origins = new[] { "http://localhost" }
            };
            var rule = new AuthorizationConfigurationValidation();
            rule.Validate(null, config).Failed.Should().BeTrue();
        }
    }
}
