using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.WebApi.Configuration
{
    public interface IWebApiConfiguration
    {
        string[] Origins { get; }

        string IssuerUrl { get; }

        string ApiName { get; }

        string ApiSecret { get; }

        string PolicyName { get; }

        Uri AuthorizationUrl { get; }

        Uri TokenUrl { get; }

        Uri RefreshUrl { get; }

        Dictionary<string, string> Scopes { get; }

        string OAuthClientId { get; }

        string OAuthAppName { get; }
    }
}