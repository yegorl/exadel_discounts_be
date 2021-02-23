using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Data.Repositories;
using Exadel.CrazyPrice.IdentityServer.Configuration;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4;

namespace Exadel.CrazyPrice.IdentityServer.Extentions
{
    public static class IdentityServerExtentions
    {
        public static IServiceCollection AddCrazyPriceIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICryptographicService, CryptographicService>();
            services.AddScoped<IProfileService, IdentityProfileService>();

            var config = new IdentityServerConfiguration(configuration);

            services.AddCors(options =>
            {
                options.AddPolicy(config.PolicyName,
                    policyBuilder =>
                    {
                        policyBuilder
                            .AllowCredentials()
                            .WithOrigins(config.Origins)
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            var builder = services.AddIdentityServer(options =>
                {
                    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                    options.EmitStaticAudienceClaim = true;
                    options.IssuerUri = config.IssuerUrl;
                })
                .AddInMemoryIdentityResources(config.IdentityResources)
                .AddInMemoryApiResources(config.ApiResources)
                .AddInMemoryApiScopes(config.ApiScopes)
                .AddInMemoryClients(config.Clients);

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme; ;

                    options.ClientId = config.GoogleClientId;
                    options.ClientSecret = config.GoogleClientSecret;
                });

            builder
                .AddSigningCredential(new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    config.CertificateName), config.CertificatePassword));

            return services;
        }

        public static IApplicationBuilder UseCrazyPriceIdentityServer(this IApplicationBuilder app, IConfiguration configuration) =>
            app
                .UseCors(new IdentityServerConfiguration(configuration).PolicyName)
                .UseIdentityServer();
    }
}
