using Exadel.CrazyPrice.WebApi.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json.Serialization;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    public static class ValidationExtentions
    {
        public static IMvcBuilder AddCrazyPriceValidation(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SearchCriteriaValidator>());
            mvcBuilder.AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            mvcBuilder.Services.Configure<ApiBehaviorOptions>(options =>
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

                    return new BadRequestObjectResult(error);
                };
            });

            return mvcBuilder;
        }

        public static IApplicationBuilder UseCrazyPriceValidation(this IApplicationBuilder app, ILogger logger)
        {
            // Force the default English messages to be used.
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            logger.LogInformation("ValidatorOptions.Global.LanguageManager.Enabled: {value}. Force the default {language} messages to be used.", false, "English");
            return app;
        }
    }
}
