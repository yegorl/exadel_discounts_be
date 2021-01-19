using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CrazyPrice API",
                    Description = "Description Web API for the site Crazy Price.",
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT license",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });
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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrazyPrice WebApi v1"));

            return app;
        }
    }
}
