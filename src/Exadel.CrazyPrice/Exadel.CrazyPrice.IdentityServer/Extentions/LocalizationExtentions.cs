using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;

namespace Exadel.CrazyPrice.IdentityServer.Extentions
{
    public static class LocalizationExtentions
    {
        public static IMvcBuilder AddCrazyPriceLocalization(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            mvcBuilder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            var config = mvcBuilder.Services.BuildServiceProvider().GetService<IConfiguration>();

            mvcBuilder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new("en"),
                    new("ru")

                };
                options.DefaultRequestCulture = new RequestCulture(config.GetLanguage());
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            return mvcBuilder;
        }

        public static IApplicationBuilder UseCrazyPriceLocalization(this IApplicationBuilder app) =>
            app.UseRequestLocalization(app
                .ApplicationServices
                .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

        private static string GetLanguage(this IConfiguration configuration) =>
            configuration.ParseSection("Localization:Default").Value;
    }
}
