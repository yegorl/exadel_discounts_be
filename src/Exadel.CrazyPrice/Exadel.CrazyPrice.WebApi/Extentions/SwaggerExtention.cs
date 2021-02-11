using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Exadel.CrazyPrice.WebApi.Filters;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    /// <summary>
    /// Determines Swagger Extentions.
    /// </summary>
    public static class SwaggerExtention
    {
        /// <summary>
        /// Adds Swagger with base description Web API for the site Crazy Price.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
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

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:8001/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:8001/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"crazypriceapi", "CrazyPrice API - protected access"}
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();

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
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CrazyPrice WebApi v1");

                options.OAuthClientId("crazy_price_api_swagger");
                options.OAuthAppName("Swagger UI for Crazy Price API");
                options.OAuthUsePkce();
            });

            return app;
        }
    }
}
