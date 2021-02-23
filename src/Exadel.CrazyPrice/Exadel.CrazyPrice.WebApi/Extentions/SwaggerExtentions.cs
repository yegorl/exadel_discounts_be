using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.WebApi.Configuration;
using Exadel.CrazyPrice.WebApi.Validators.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    /// <summary>
    /// Determines Swagger Extentions.
    /// </summary>
    public static class SwaggerExtentions
    {
        /// <summary>
        /// Adds Swagger with base description Web API for the site Crazy Price.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwaggerConfiguration>(configuration.GetSection("Swagger"));
            services.TryAddSingleton<IValidateOptions<SwaggerConfiguration>, SwaggerConfigurationValidation>();

            services.AddSwaggerGen();

            services
                .AddOptions<SwaggerGenOptions>()
                .Configure<IOptionsMonitor<SwaggerConfiguration>>((options, config) =>
                {
                    var swaggerConfig = config.CurrentValue;
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Protected CrazyPrice API",
                        Description = "Description Web API for the site Crazy Price.",
                        License = new OpenApiLicense
                        {
                            Name = "Use under MIT license",
                            Url = new Uri("https://opensource.org/licenses/MIT"),
                        }
                    });

                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows =
                            new OpenApiOAuthFlows
                            {
                                AuthorizationCode =
                                    new OpenApiOAuthFlow
                                    {
                                        AuthorizationUrl = swaggerConfig.AuthorizationUrl.ToUri(),
                                        TokenUrl = swaggerConfig.TokenUrl.ToUri(),
                                        RefreshUrl = swaggerConfig.RefreshUrl.ToUri(UriKind.Absolute, false),
                                        Scopes = swaggerConfig.Scopes
                                    }
                            }
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "oauth2",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new[] { swaggerConfig.ApiName }
                        }
                    });

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                });

            services
                .AddOptions<SwaggerUIOptions>()
                .Configure<IOptionsMonitor<SwaggerConfiguration>>((options, config) =>
                {
                    var swaggerConfig = config.CurrentValue;
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "CrazyPrice WebApi v1");

                    options.OAuthClientId(swaggerConfig.OAuthClientId);
                    options.OAuthAppName(swaggerConfig.OAuthAppName);
                    options.OAuthUsePkce();
                });

            return services;
        }

        /// <summary>
        /// Uses Swagger and UseSwaggerUI Middleware.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerCrazyPrice(this IApplicationBuilder app) =>
            app
                .UseSwagger()
                .UseSwaggerUI();
    }
}
