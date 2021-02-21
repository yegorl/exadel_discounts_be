using Exadel.CrazyPrice.WebApi.Configuration;
using Exadel.CrazyPrice.WebApi.Validators.Configuration;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    public static class AuthenticationExtentions
    {
        public static IServiceCollection AddCrazyPriceAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthorizationConfiguration>(configuration.GetSection("Authorization"));
            services.TryAddSingleton<IValidateOptions<AuthorizationConfiguration>, AuthorizationConfigurationValidation>();

            services.AddCors();
            services
                .AddAuthentication(options =>
                        {
                            options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultForbidScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                            options.DefaultSignOutScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                        })
                .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme);

            services
                .AddOptions<Microsoft.AspNetCore.Cors.Infrastructure.CorsOptions>()
                .Configure<IOptionsMonitor<AuthorizationConfiguration>>((options, config) =>
                {
                    var authorizationConfig = config.CurrentValue;
                    options.AddPolicy(authorizationConfig.PolicyName, builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowCredentials()
                            .WithOrigins(authorizationConfig.Origins)
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                });

            services.AddOptions<IdentityServerAuthenticationOptions>(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .Configure<IOptionsMonitor<AuthorizationConfiguration>>((options, config) =>
                {
                    var authorizationConfig = config.CurrentValue;
                    options.Authority = authorizationConfig.IssuerUrl;
                    options.ApiName = authorizationConfig.ApiName;
                    options.ApiSecret = authorizationConfig.ApiSecret;
                    options.IntrospectionDiscoveryPolicy = authorizationConfig.IntrospectionDiscoveryPolicy;
                });

            return services;
        }

        public static IApplicationBuilder UseCrazyPriceAuthentication(this IApplicationBuilder app) =>
            app
                .UseCors(app.ApplicationServices.GetService<IOptionsMonitor<AuthorizationConfiguration>>().CurrentValue.PolicyName)
                .UseAuthentication();
    }
}
