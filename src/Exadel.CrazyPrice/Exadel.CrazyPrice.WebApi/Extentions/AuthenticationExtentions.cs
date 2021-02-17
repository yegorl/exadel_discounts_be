﻿using Exadel.CrazyPrice.WebApi.Configuration;
using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    public static class AuthenticationExtentions
    {
        public static IServiceCollection AddCrazyPriceAuthentication(this IServiceCollection services)
        {
            services.TryAddSingleton<IWebApiConfiguration, WebApiConfiguration>();

            var config = services.BuildServiceProvider().GetService<IWebApiConfiguration>();

            services.AddCors(options =>
            {
                options.AddPolicy(config.PolicyName,
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowCredentials()
                            .WithOrigins(config.Origins)
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultForbidScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignOutScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = config.IssuerUrl;
                    options.ApiName = config.ApiName;
                    options.ApiSecret = config.ApiSecret;

                    options.IntrospectionDiscoveryPolicy = new DiscoveryPolicy { ValidateEndpoints = false };
                });

            return services;
        }

        public static IApplicationBuilder UseCrazyPriceAuthentication(this IApplicationBuilder app)
        {
            return
                app
                .UseCors(app.ApplicationServices.GetService<IWebApiConfiguration>().PolicyName)
                .UseAuthentication();
        }
    }
}