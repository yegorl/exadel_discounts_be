using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.WebApi.Configuration
{
    public class WebApiConfiguration : IWebApiConfiguration
    {
        private readonly IConfiguration _config;

        public WebApiConfiguration(IConfiguration config)
        {
            _config = config;
        }

        public string[] Origins =>
            _config.GetArrayString("Auth:Origins");

        public string IssuerUrl =>
            _config.GetString("Auth:IssuerUrl");

        public string ApiName =>
            _config.GetString("Auth:ApiName");

        public string ApiSecret =>
            _config.GetString("Auth:ApiSecret");

        public string PolicyName =>
            _config.GetString("Auth:PolicyName");

        public Uri AuthorizationUrl =>
            _config.GetUri("Auth:Swagger:AuthorizationUrl");

        public Uri TokenUrl =>
            _config.GetUri("Auth:Swagger:TokenUrl");

        public Uri RefreshUrl =>
            _config.GetUri("Auth:Swagger:RefreshUrl");

        public Dictionary<string, string> Scopes =>
            _config.GetDictionaryString("Auth:Swagger:Scopes");

        public string OAuthClientId =>
            _config.GetString("Auth:Swagger:OAuthClientId");

        public string OAuthAppName =>
            _config.GetString("Auth:Swagger:OAuthAppName");
    }
}
