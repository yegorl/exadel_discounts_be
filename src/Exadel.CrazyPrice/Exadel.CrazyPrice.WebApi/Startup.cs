using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.WebApi.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Exadel.CrazyPrice.WebApi
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
        /// Gets Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers()
                .AddCrazyPriceValidation();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddCrazyPriceAuthentication(Configuration);

            if (!WebHostEnvironment.IsProduction())
            {
                services.AddSwagger(Configuration);
            }

            services.AddMongoDb();
        }

        /// <summary>
        /// Gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="logger"></param>
        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            var appName = assemblyName.Name;
            var appVersion = assemblyName.Version;
            logger.LogInformation("Starting {appName} v{appVersion}", appName, appVersion);

            logger.LogInformation("Used {environment} mode.", WebHostEnvironment.EnvironmentName);

            if (WebHostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logger.LogInformation("Used developer exception page.");
            }

            if (!WebHostEnvironment.IsProduction())
            {
                app.UseSwaggerCrazyPrice();
                LogInfo(logger, "swagger");
            }

            app.UseCrazyPriceValidation(logger);
            LogInfo(logger, "validation");


            app.UseHttpsRedirection();
            LogInfo(logger, "https redirection");

            app.UseCrazyPriceAuthentication();
            LogInfo(logger, "authentication");

            app.UseRouting();
            LogInfo(logger, "routing");

            app.UseAuthorization();
            LogInfo(logger, "authorization");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            LogInfo(logger, "endpoints");
        }

        private static void LogInfo(ILogger logger, string message)
        {
            logger.LogInformation("Used {value}.", message);
        }
    }
}
