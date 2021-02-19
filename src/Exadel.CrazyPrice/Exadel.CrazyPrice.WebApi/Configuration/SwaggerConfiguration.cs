using System.Collections.Generic;

namespace Exadel.CrazyPrice.WebApi.Configuration
{
    public class SwaggerConfiguration
    {
        public string AuthorizationUrl { get; set; }

        public string TokenUrl { get; set; }

        public string RefreshUrl { get; set; }

        public Dictionary<string, string> Scopes { get; set; }

        public string ApiName { get; set; }

        public string OAuthClientId { get; set; }

        public string OAuthAppName { get; set; }
    }
}
