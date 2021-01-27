// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;
using Microsoft.Extensions.Configuration;

namespace Exadel.CrazyPrice.IdentityServer
{
    public static class Config
    {
        public static IConfiguration Configuration { get; set; }
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(
                    "role",
                    "Your role(s)",
                    new List<string>(){"role"})
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("crazypriceapi", 
                    "Crazy Price API", 
                    new List<string>() { "role" })
                {
                    Scopes = { "crazypriceapi"},
                    ApiSecrets = { new Secret("apisecret".Sha256())}
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("crazypriceapi", "Crazy Price API scope")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 120,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientName = "Crazy Price",
                    ClientId = "crazypriceclient1",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RedirectUris = new List<string>()
                    {
                        
                        "https://localhost:44357",
                        "https://localhost:44357/callback.html",
                        "https://localhost:44357/silent-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44357/",
                        "https://localhost:44357"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role",
                        "crazypriceapi"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                },
                new Client
                {
                    ClientName = "vuejs_code_client",
                    ClientId = "crazypriceclient",
                    AccessTokenType = AccessTokenType.Reference,
                    // RequireConsent = false,
                    AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 300,

                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44357",
                        "https://localhost:44357/callback.html",
                        "https://localhost:44357/silent-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44357/",
                        "https://localhost:44357"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44357"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role",
                        "crazypriceapi"
                    }
                },
                new Client
                {
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 120,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    ClientName = "Crazy Price Test",
                    ClientId = "crazypricetestclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44325",
                        "https://localhost:44325/signin-oidc",
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44325/signout-callback-oidc",
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role",
                        "crazypriceapi"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }

            };
    }
}