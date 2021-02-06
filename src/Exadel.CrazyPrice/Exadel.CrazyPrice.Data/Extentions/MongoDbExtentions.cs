using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Exadel.CrazyPrice.Common.Configurations;

namespace Exadel.CrazyPrice.Data.Extentions
{
    public static class MongoDbExtentions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            MongoDbConvention.SetCamelCaseElementNameConvention();

            services.AddSingleton<IDbConfiguration, MongoDbConfiguration>();
            services.AddSingleton<IDiscountRepository, DiscountRepository>();
            services.AddSingleton<IAddressRepository, QuickSearchRepository>();
            services.AddSingleton<ICompanyRepository, QuickSearchRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITagRepository, QuickSearchRepository>();

            return services;
        }

        public static Dictionary<string, string> CompileQuery(this Dictionary<string, string> queryParams)
        {
            string translations;

            if (!queryParams.Keys.Contains("ignoreLanguage") || queryParams["ignoreLanguage"].ToLowerInvariant() == "false")
            {
                translations = LanguageOption.En == queryParams["searchValue"].GetLanguageFromFirstLetter()
                                ? "translations." : string.Empty;
            }
            else
            {
                translations = string.Empty;
            }

            foreach (var (key, value) in queryParams)
            {
                if (key.StartsWith("search", StringComparison.OrdinalIgnoreCase))
                {
                    queryParams["query"] = queryParams["query"].Replace($"%{key}%", value);
                    continue;
                }

                if (key.StartsWith("field", StringComparison.OrdinalIgnoreCase))
                {
                    queryParams[key] = $"{translations}{value}";
                    queryParams["query"] = queryParams["query"].Replace($"%{key}%", queryParams[key]);
                }
            }

            return queryParams;
        }
    }
}
