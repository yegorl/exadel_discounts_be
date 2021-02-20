using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.IdentityServer.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Exadel.CrazyPrice.IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        /// <summary>
        /// Gets Configuration
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddCrazyPriceLocalization();

            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            services.AddCrazyPriceIdentityServer(Configuration);

            services.AddMongoDb();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCrazyPriceIdentityServer(Configuration);

            // uncomment, if you want to add MVC
            app.UseAuthorization();

            app.UseCrazyPriceLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
