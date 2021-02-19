using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Data.Repositories;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Services;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;

namespace Exadel.CrazyPrice.IdentityServer.Extentions
{
    public static class IdentityServerExtentions
    {
        public static IServiceCollection AddCrazyPriceIdentityServer(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICryptographicService, CryptographicService>();
            services.AddScoped<IProfileService, IdentityProfileService>();

            var config = services.BuildServiceProvider().GetService<IConfiguration>();

            services.AddCors(options =>
            {
                options.AddPolicy(config.GetPolicyName(),
                    policyBuilder =>
                    {
                        policyBuilder
                            .AllowCredentials()
                            .WithOrigins(config.GetOrigins())
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            
            var builder = services.AddIdentityServer(options =>
                {
                    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                    options.EmitStaticAudienceClaim = true;
                    options.IssuerUri = config.GetIssuerUrl();
                })
                .AddInMemoryIdentityResources(config.GetIdentityResources())
                .AddInMemoryApiResources(config.GetApiResources())
                .AddInMemoryApiScopes(config.GetApiScopes())
                .AddInMemoryClients(config.GetClients());

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "458404438508-18deveqet26htfakoq6v9lh54ecfja12.apps.googleusercontent.com";
                    options.ClientSecret = "_h9XlzmxjbVVhoC9LMU2CN6U";
                }).AddLocalApi(options =>
                {
                    options.ExpectedScope = "crazy_price_api1";
                });

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            //var clientCertificate =
            //    new X509Certificate2(Path.Combine(
            //        Environment.ContentRootPath, configuration.Get["CertificateName"].First()),
            //        configuration.Get["CertificatePassword"].First());

            //builder.AddSigningCredential(clientCertificate);

            return services;
        }

        public static IApplicationBuilder UseCrazyPriceIdentityServer(this IApplicationBuilder app) =>
            app
                .UseCors(app.ApplicationServices.GetService<IConfiguration>().GetPolicyName())
                .UseIdentityServer();

        private static IEnumerable<Client> GetClients(this IConfiguration config) =>
            config.GetSection("Clients").Get<List<Client>>();

        private static IEnumerable<ApiScope> GetApiScopes(this IConfiguration config) =>
            config.GetSection("ApiScopes").Get<List<ApiScope>>();

        private static IEnumerable<ApiResource> GetApiResources(this IConfiguration config) =>
            config.GetSection("ApiResources").Get<List<ApiResource>>();

        private static IEnumerable<IdentityResource> GetIdentityResources(this IConfiguration config) =>
            config.GetSection("IdentityResources").Get<List<IdentityResource>>();

        private static string GetCertificateName(this IConfiguration config) =>
            config.GetSection("Certificate:Name").Value;

        private static string GetCertificatePassword(this IConfiguration config) =>
            config.GetSection("Certificate:Password").Value;
    }
}
