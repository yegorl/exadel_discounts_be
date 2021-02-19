using System.Collections.Generic;
using IdentityServer4.Models;

namespace Exadel.CrazyPrice.IdentityServer.Configuration
{
    public interface IIdentityServerConfiguration
    {
        string[] Origins { get; }

        string PolicyName { get; }

        string IssuerUrl { get; }

        IEnumerable<Client> Clients { get; }

        IEnumerable<ApiScope> ApiScopes { get; }

        IEnumerable<ApiResource> ApiResources { get; }

        IEnumerable<IdentityResource> IdentityResources { get; }

        string CertificateName { get; }

        string CertificatePassword { get; }
    }
}