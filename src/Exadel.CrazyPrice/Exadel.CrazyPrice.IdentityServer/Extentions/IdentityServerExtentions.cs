using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Data.Repositories;
using Exadel.CrazyPrice.IdentityServer.Configuration;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddSingleton<IIdentityServerConfiguration, IdentityServerConfiguration>();

            var config = services.BuildServiceProvider().GetService<IIdentityServerConfiguration>();

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
                .UseCors(app.ApplicationServices.GetService<IIdentityServerConfiguration>().PolicyName)
                .UseIdentityServer();
    }
}
