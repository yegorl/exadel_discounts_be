using Exadel.CrazyPrice.Common.Extentions;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.IdentityServer.Configuration
{
    public class IdentityServerConfiguration
    {
        private readonly IConfiguration _config;

        public IdentityServerConfiguration(IConfiguration config)
        {
            _config = config;
        }

        public string[] Origins =>
            _config.GetArrayString("IdentityServer:Origins");

        public string PolicyName =>
            _config.GetString("IdentityServer:PolicyName");

        public string IssuerUrl =>
            _config.GetString("IdentityServer:IssuerUrl");

        public IEnumerable<Client> Clients =>
            _config.ParseSection("IdentityServer:Clients").Get<List<Client>>();

        public IEnumerable<ApiScope> ApiScopes =>
            _config.ParseSection("IdentityServer:ApiScopes").Get<List<ApiScope>>();

        public IEnumerable<ApiResource> ApiResources =>
            _config.ParseSection("IdentityServer:ApiResources").Get<List<ApiResource>>();

        public IEnumerable<IdentityResource> IdentityResources =>
            _config.ParseSection("IdentityServer:IdentityResources").Get<List<IdentityResource>>();
        public string CertificateName =>
            _config.GetString("Certificate:Name");

        public string CertificatePassword =>
            _config.GetString("Certificate:Password");
    }
}
