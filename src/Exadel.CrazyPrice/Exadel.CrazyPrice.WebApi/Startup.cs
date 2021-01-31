using Exadel.CrazyPrice.WebApi.Extentions;
using Exadel.CrazyPrice.WebApi.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json.Serialization;
using Exadel.CrazyPrice.Data.Extentions;

namespace Exadel.CrazyPrice.WebApi
{
    /// <summary>
    /// Determines Startup configuration.
    /// </summary>
    public class Startup
    {
        private ILogger<Startup> _logger;

        /// <summary>
        /// Creates Startup configuration.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        /// <summary>
        /// Gets Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonValidator>());

            services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var (key, value) =
                        context.ModelState.First(e => e.Value.ValidationState == ModelValidationState.Invalid);

                    var error = value.Errors[0].ErrorMessage;

                    #region ForPrimitiveType
                    // Needed if the controller gets a primitive type like string or int etc.
                    if (error.Contains("''"))
                    {
                        error = error.Replace("''", $"'{key}'");
                    }
                    #endregion

                    _logger.LogError("Validation error: {error}", error);
                    return new BadRequestObjectResult(error);
                };
            });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            services.AddSwagger();

            services.AddMongoDb(options =>
            {
                options.ConnectionString = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
                options.Database = Configuration.GetSection("ConnectionStrings:Database").Value;
            });
        }

        /// <summary>
        /// Gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            _logger = logger;
            // Force the default English messages to be used.
            ValidatorOptions.Global.LanguageManager.Enabled = false;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerCrazyPrice();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
