using Exadel.CrazyPrice.Common.Extentions;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.IdentityServer.Configuration
{
    public class IdentityServerConfiguration : IIdentityServerConfiguration
    {
        private readonly IConfiguration _config;

        public IdentityServerConfiguration(IConfiguration config)
        {
            _config = config;
        }

        public string[] Origins =>
            _config.GetArrayString("Auth:Origins");

        public string PolicyName =>
            _config.GetString("Auth:PolicyName");

        public string IssuerUrl =>
            _config.GetString("Auth:IssuerUrl");

        public IEnumerable<Client> Clients =>
            _config.GetSection("Clients").Get<List<Client>>();

        public IEnumerable<ApiScope> ApiScopes =>
            _config.GetSection("ApiScopes").Get<List<ApiScope>>();

        public IEnumerable<ApiResource> ApiResources =>
            _config.GetSection("ApiResources").Get<List<ApiResource>>();

        public IEnumerable<IdentityResource> IdentityResources =>
            _config.GetSection("IdentityResources").Get<List<IdentityResource>>();

        public string CertificateName =>
            _config.GetString("Certificate:Name");

        public string CertificatePassword =>
            _config.GetString("Certificate:Password");
    }
}
