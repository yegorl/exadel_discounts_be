using IdentityModel.Client;

namespace Exadel.CrazyPrice.WebApi.Configuration
{
    public class AuthorizationConfiguration
    {
        public string ApiName { get; set; }

        public string ApiSecret { get; set; }

        public string PolicyName { get; set; }

        public string IssuerUrl { get; set; }

        public string[] Origins { get; set; }

        public DiscoveryPolicy IntrospectionDiscoveryPolicy { get; set; }
    }
}
