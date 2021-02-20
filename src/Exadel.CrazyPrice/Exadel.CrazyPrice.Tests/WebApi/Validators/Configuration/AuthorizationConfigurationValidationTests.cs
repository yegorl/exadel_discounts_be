using Exadel.CrazyPrice.WebApi.Configuration;
using Exadel.CrazyPrice.WebApi.Validators.Configuration;
using FluentAssertions;
using IdentityModel.Client;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Validators.Configuration
{
    public class AuthorizationConfigurationValidationTests
    {
        [Fact]
        public void AuthorizationConfigurationValidationSucceededTest()
        {
            var config = new AuthorizationConfiguration
            {
                PolicyName = "PolicyName",
                ApiName = "ApiName",
                ApiSecret = "ApiSecret",
                IssuerUrl = "IssuerUrl",
                Origins = new[] { "http://localhost" },
                IntrospectionDiscoveryPolicy = new DiscoveryPolicy
                {
                    RequireHttps = true,
                    RequireKeySet = true,
                    AllowHttpOnLoopback = true,
                    ValidateIssuerName = true,
                    ValidateEndpoints = true,
                    Authority = "https://identityserver:443",
                    AdditionalEndpointBaseAddresses = { "https://identityserver:443", "https://identityserver" }
                }
            };

            var rule = new AuthorizationConfigurationValidation();
            rule.Validate(null, config).Succeeded.Should().BeTrue();
        }

        [Fact]
        public void AuthorizationConfigurationValidationFailTest()
        {
            var config = new AuthorizationConfiguration
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
            var config = new AuthorizationConfiguration
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

        [Theory]
        [InlineData(true, true, true, true, true)]
        public void AuthorizationConfigurationValidationIntrospectionDiscoveryPolicyAdditionalEndpointBaseAddressesManyFailTest(
            bool requireHttps,
            bool requireKeySet,
            bool allowHttpOnLoopback,
            bool validateIssuerName,
            bool validateEndpoints
            )
        {
            var config = new AuthorizationConfiguration
            {
                PolicyName = "policyName",
                ApiName = "apiName",
                ApiSecret = "apiSecret",
                IssuerUrl = "issuerUrl",
                Origins = new[] { "http://localhost" },
                IntrospectionDiscoveryPolicy = new DiscoveryPolicy
                {
                    RequireHttps = requireHttps,
                    RequireKeySet = requireKeySet,
                    AllowHttpOnLoopback = allowHttpOnLoopback,
                    ValidateIssuerName = validateIssuerName,
                    ValidateEndpoints = validateEndpoints,
                    AdditionalEndpointBaseAddresses = { "https://identityserver:443\\", "https://identityserver/" },
                    Authority = "https://identityserver:443/"
                }
            };
            var rule = new AuthorizationConfigurationValidation();
            rule.Validate(null, config).Failed.Should().BeTrue();
        }

        [Theory]
        [InlineData(true, true, true, true, true)]
        public void AuthorizationConfigurationValidationIntrospectionDiscoveryPolicyAuthorityManyFailTest(
            bool requireHttps,
            bool requireKeySet,
            bool allowHttpOnLoopback,
            bool validateIssuerName,
            bool validateEndpoints
        )
        {
            var config = new AuthorizationConfiguration
            {
                PolicyName = "policyName",
                ApiName = "apiName",
                ApiSecret = "apiSecret",
                IssuerUrl = "issuerUrl",
                Origins = new[] { "http://localhost" },
                IntrospectionDiscoveryPolicy = new DiscoveryPolicy
                {
                    RequireHttps = requireHttps,
                    RequireKeySet = requireKeySet,
                    AllowHttpOnLoopback = allowHttpOnLoopback,
                    ValidateIssuerName = validateIssuerName,
                    ValidateEndpoints = validateEndpoints,
                    AdditionalEndpointBaseAddresses = { "https://identityserver:443", "https://identityserver" },
                    Authority = "https://identityserver:443/"
                }
            };
            var rule = new AuthorizationConfigurationValidation();
            rule.Validate(null, config).Failed.Should().BeTrue();
        }

        [Fact]
        public void AuthorizationConfigurationValidationIntrospectionDiscoveryPolicyNullFailTest()
        {
            var config = new AuthorizationConfiguration
            {
                PolicyName = "policyName",
                ApiName = "apiName",
                ApiSecret = "apiSecret",
                IssuerUrl = "issuerUrl",
                Origins = new[] { "http://localhost" },
                IntrospectionDiscoveryPolicy = null
            };
            var rule = new AuthorizationConfigurationValidation();
            rule.Validate(null, config).Failed.Should().BeTrue();
        }
    }
}
