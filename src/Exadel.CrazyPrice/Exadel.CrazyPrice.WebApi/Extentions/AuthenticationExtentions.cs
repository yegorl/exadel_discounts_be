using Exadel.CrazyPrice.Common.Extentions;
using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    public static class AuthenticationExtentions
    {
        public static IServiceCollection AddCrazyPriceAuthentication(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IConfiguration>();

            services.AddCors(options =>
            {
                options.AddPolicy(config.GetPolicyName(),
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowCredentials()
                            .WithOrigins(config.GetOrigins())
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
                    options.Authority = config.GetIssuerUrl();
                    options.ApiName = config.GetApiName();
                    options.ApiSecret = config.GetApiSecret();

                    options.IntrospectionDiscoveryPolicy = new DiscoveryPolicy { ValidateEndpoints = false };
                });

            return services;
        }

        public static IApplicationBuilder UseCrazyPriceAuthentication(this IApplicationBuilder app) =>
            app
                .UseCors(app.ApplicationServices.GetService<IConfiguration>().GetPolicyName())
                .UseAuthentication();

        private static string GetApiName(this IConfiguration config) =>
            config.GetSection("Auth:ApiName").Value;

        private static string GetApiSecret(this IConfiguration config) =>
            config.GetSection("Auth:ApiSecret").Value;
    }
}
