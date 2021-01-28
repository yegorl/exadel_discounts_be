using IdentityServer4.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Exadel.CrazyPrice.Common.Configurations
{
    public interface IIdentityServerConfiguration
    {
        public IdentityServerOptions IdentityServerOptions { get; }

        public IEnumerable<ApiScope> ApiScopes { get; }

        public IEnumerable<Client> Clients { get; }

        public IEnumerable<ApiResource> ApiResources { get; }

        public IEnumerable<IdentityResource> IdentityResources { get; }

        public List<TestUser> TestUsers { get; }

        public X509Certificate2 Certificate { get; }
    }
}
