using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace Exadel.CrazyPrice.Tests.IdentityServer.Configuration
{
    public class ConfigurationTests
    {
        [Fact]
        public void ClientJsonTest()
        {
            var value = new List<Client>
            {
                new Client
                {
                    AccessTokenType = AccessTokenType.Reference,
                    // RequireConsent = false,
                    AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    AllowAccessTokensViaBrowser = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44357"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role",
                        "crazy_price_api1"
                    },

                    ClientName = "vuejs_code_client",
                    ClientId = "crazypriceclient",

                    IdentityTokenLifetime = 300,

                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44357/",
                        "https://localhost:44357"
                    },

                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44357",
                        "https://localhost:44357/callback.html",
                        "https://localhost:44357/silent-renew.html"
                    }
                },
                new Client
                {
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 120,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role",
                        "crazy_price_api1"
                    },
                    AllowOfflineAccess = true,

                    ClientId = "crazypricetestclient",
                    ClientName = "Crazy Price Test",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44325/signout-callback-oidc",
                    },

                    RequirePkce = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44325",
                        "https://localhost:44325/signin-oidc",
                    },

                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "crazy_price_api1" },

                    // no interactive user, use the clientid/secret for authentication
                    ClientId = "crazy_price_api",
                    ClientName = "Crazy Price API",
                    ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        }
                },
                new Client
                {
                    AccessTokenType = AccessTokenType.Reference,
                    AllowedCorsOrigins = { "https://localhost:44389" },
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role",
                        "crazy_price_api1"
                    },

                    ClientId = "crazy_price_api_swagger",
                    ClientName = "Swagger UI for Crazy Price API",
                    //ClientSecrets =
                    //{
                    //    new Secret("secret".Sha256())
                    //},

                    RedirectUris = { "https://localhost:44389/swagger/oauth2-redirect.html" },
                    //RedirectUris = { "https://localhost:9001/swagger/oauth2-redirect.html" },
                    RequirePkce = true,
                    RequireClientSecret = false,

                    PostLogoutRedirectUris = { "https://localhost:44389/signout-callback-oidc" },
                    //PostLogoutRedirectUris = { "https://localhost:9001/signout-callback-oidc" },

                    UpdateAccessTokenClaimsOnRefresh = true
                }
                };

            //var value = new Secret("secret".Sha256());

            //var value =
            //    new[]
            //    {
            //        new IdentityResources.OpenId(),
            //        new IdentityResources.Profile(),
            //        new IdentityResource("role", "Your role(s)", new List<string> { "role" })
            //    };

            //var value = new OpenApiOAuthFlows
            //{
            //    AuthorizationCode = new OpenApiOAuthFlow
            //    {
            //        AuthorizationUrl = new Uri("https://identityserver:443/connect/authorize"),
            //        TokenUrl = new Uri("https://identityserver:443/connect/token"),

            //        Scopes = new Dictionary<string, string>
            //    {
            //        {"crazy_price_api1", "CrazyPrice API - protected access"}
            //    }
            //    }
            //};

            var jsonString = JsonSerializer.Serialize(value);

            //Assert
            Assert.IsType<string>(jsonString);
        }
    }
}
