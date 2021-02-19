using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.WebApi.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace Exadel.CrazyPrice.WebApi.Validators.Configuration
{
    public class SwaggerConfigurationValidation : IValidateOptions<SwaggerConfiguration>
    {
        public ValidateOptionsResult Validate(string name, SwaggerConfiguration options)
        {
            if (options.AuthorizationUrl.IsNullOrEmptyOrLast(StringComparison.Ordinal, "\\", "/"))
            {
                return ValidateOptionsResult.Fail("The AuthorizationUrl must be defined and the last character must not be a '/' or '\\'.");
            }

            if (options.TokenUrl.IsNullOrEmptyOrLast(StringComparison.Ordinal, "\\", "/"))
            {
                return ValidateOptionsResult.Fail("The TokenUrl must be defined and the last character must not be a '/' or '\\'.");
            }

            if (!options.RefreshUrl.IsNullOrEmpty() && options.RefreshUrl.IsLast(StringComparison.Ordinal, "\\", "/"))
            {
                return ValidateOptionsResult.Fail("The RefreshUrl must be defined and the last character must not be a '/' or '\\'.");
            }

            if (options.ApiName.IsNullOrEmpty())
            {
                return ValidateOptionsResult.Fail("The ApiName must be defined.");
            }

            if (options.OAuthClientId.IsNullOrEmpty())
            {
                return ValidateOptionsResult.Fail("The OAuthClientId must be defined.");
            }

            if (options.OAuthAppName.IsNullOrEmpty())
            {
                return ValidateOptionsResult.Fail("The OAuthAppName must be defined.");
            }

            if (options.Scopes.IsNullOrEmpty())
            {
                return ValidateOptionsResult.Fail("The Scopes must be defined. Format: \"scope_name\": \"description\". ");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
