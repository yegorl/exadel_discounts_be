using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
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
            var config = services.BuildServiceProvider().GetService<IConfiguration>();

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
                                    AuthorizationUrl = config.GetAuthorizationUrl(),
                                    TokenUrl = config.GetTokenUrl(),
                                    RefreshUrl = config.GetRefreshUrl(),
                                    Scopes = config.GetScopes()
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
                        new[] { config.GetApiName() }
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
            var config = app.ApplicationServices.GetService<IConfiguration>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CrazyPrice WebApi v1");

                options.OAuthClientId(config.GetOAuthClientId());
                options.OAuthAppName(config.GetOAuthAppName());
                options.OAuthUsePkce();
            });

            return app;
        }

        private static Uri GetAuthorizationUrl(this IConfiguration config) =>
            config.GetUri("Auth:Swagger:AuthorizationUrl");

        private static Uri GetTokenUrl(this IConfiguration config) =>
            config.GetUri("Auth:Swagger:TokenUrl");

        private static Uri GetRefreshUrl(this IConfiguration config) =>
            config.GetUri("Auth:Swagger:RefreshUrl");

        private static Dictionary<string, string> GetScopes(this IConfiguration config) =>
            config.GetSection("Auth:Swagger:Scopes").Get<Dictionary<string, string>>();

        private static string GetOAuthClientId(this IConfiguration config) =>
            config.GetOption("Auth:Swagger:OAuthClientId");

        private static string GetOAuthAppName(this IConfiguration config) =>
            config.GetOption("Auth:Swagger:OAuthAppName");

        private static string GetApiName(this IConfiguration config) =>
            config.GetOption("Auth:ApiName");

        private static Uri GetUri(this IConfiguration config, string key) =>
           string.IsNullOrEmpty(config.GetOption(key)) ? null : new Uri(config.GetOption(key), UriKind.Absolute);
    }
}
