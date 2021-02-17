using Exadel.CrazyPrice.WebApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
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
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.TryAddSingleton<IWebApiConfiguration, WebApiConfiguration>();

            var config = services.BuildServiceProvider().GetService<IWebApiConfiguration>();

            services.AddSwaggerGen(options =>
            {
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
                                    AuthorizationUrl = config.AuthorizationUrl,
                                    TokenUrl = config.TokenUrl,
                                    RefreshUrl = config.RefreshUrl,
                                    Scopes = config.Scopes
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
                        new[] { config.ApiName }
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });

            return services;
        }

        /// <summary>
        /// Uses Swagger and UseSwaggerUI Middleware.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerCrazyPrice(this IApplicationBuilder app)
        {
            var config = app.ApplicationServices.GetService<IWebApiConfiguration>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CrazyPrice WebApi v1");

                options.OAuthClientId(config.OAuthClientId);
                options.OAuthAppName(config.OAuthAppName);
                options.OAuthUsePkce();
            });

            return app;
        }
    }
}
