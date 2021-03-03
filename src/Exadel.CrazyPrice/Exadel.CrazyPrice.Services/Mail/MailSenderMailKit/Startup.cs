using Exadel.CrazyPrice.Services.Bus.IntegrationBus.Extentions;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit
{
    /// <summary>
    /// Determines Startup configuration.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Creates Startup configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="webHostEnvironment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Gets Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets WebHostEnvironment.
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRabbitMQ(Configuration)
                .AddEventBus(Configuration)
                .AddEventBusServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello!");
                });
            });

            app.UseEventBus();
        }
    }
}
