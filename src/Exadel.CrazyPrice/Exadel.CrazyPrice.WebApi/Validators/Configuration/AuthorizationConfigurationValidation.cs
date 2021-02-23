using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.WebApi.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Exadel.CrazyPrice.WebApi.Validators.Configuration
{
    public class AuthorizationConfigurationValidation : IValidateOptions<AuthorizationConfiguration>
    {
        public ValidateOptionsResult Validate(string name, AuthorizationConfiguration options)
        {
            if (options.ApiName.IsNullOrEmpty())
            {
                return ValidateOptionsResult.Fail("The ApiName must be defined.");
            }

            if (options.ApiSecret.IsNullOrEmpty())
            {
                return ValidateOptionsResult.Fail("The ApiSecret must be defined.");
            }

            if (options.PolicyName.IsNullOrEmpty())
            {
                return ValidateOptionsResult.Fail("The PolicyName must be defined.");
            }

            if (options.IssuerUrl.IsNullOrEmptyOrLast(StringComparison.Ordinal, "\\", "/"))
            {
                return ValidateOptionsResult.Fail("The IssuerUrl must be defined and the last character must not be a '/' or '\\'.");
            }

            if (options.Origins.IsNullOrEmpty() ||
                options.Origins.Any(o => o.IsNullOrEmptyOrLast(StringComparison.Ordinal, "\\", "/")))
            {
                return ValidateOptionsResult.Fail("The Origins must be defined and the last character must not be a '/' or '\\'.");
            }

            if (options.IntrospectionDiscoveryPolicy == null)
            {
                return ValidateOptionsResult.Fail("The IntrospectionDiscoveryPolicy must be defined.");
            }

            if (options.IntrospectionDiscoveryPolicy.ValidateEndpoints &&
                (!options.IntrospectionDiscoveryPolicy.AdditionalEndpointBaseAddresses.Any() ||
                 options.IntrospectionDiscoveryPolicy.AdditionalEndpointBaseAddresses
                     .Any(o => o.IsNullOrEmptyOrLast(StringComparison.Ordinal, "\\", "/"))))
            {
                return ValidateOptionsResult.Fail(
                    "IntrospectionDiscoveryPolicy.AdditionalEndpointBaseAddresses must be defined when IntrospectionDiscoveryPolicy.ValidateEndpoints is true. " +
                    "The last character each string of IntrospectionDiscoveryPolicy.AdditionalEndpointBaseAddresses must not be a '/' or '\\'.");
            }

            if (options.IntrospectionDiscoveryPolicy.ValidateIssuerName && options.IntrospectionDiscoveryPolicy.Authority.IsNullOrEmptyOrLast(StringComparison.Ordinal, "\\", "/"))
            {
                return ValidateOptionsResult.Fail("The IntrospectionDiscoveryPolicy.Authority must be defined " +
                                                  "when IntrospectionDiscoveryPolicy.ValidateIssuerName is true. " +
                                                  "The last character of IntrospectionDiscoveryPolicy.Authority must not be a '/' or '\\'.");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
