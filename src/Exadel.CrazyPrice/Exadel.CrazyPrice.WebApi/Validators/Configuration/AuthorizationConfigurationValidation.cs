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

            return ValidateOptionsResult.Success;
        }
    }
}
