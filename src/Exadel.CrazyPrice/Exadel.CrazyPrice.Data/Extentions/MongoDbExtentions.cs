using Exadel.CrazyPrice.Common.Configurations;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.CrazyPrice.Data.Extentions
{
    /// <summary>
    /// Represents the method for add MongoDb.
    /// </summary>
    public static class MongoDbExtentions
    {
        /// <summary>
        /// Gets MongoDb services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMongoDb(this IServiceCollection services)
        {
            MongoDbConvention.SetCamelCaseElementNameConvention();

            services.AddSingleton<IDbConfiguration, MongoDbConfiguration>();
            services.AddSingleton<IDiscountRepository, DiscountRepository>();
            services.AddSingleton<IAddressRepository, QuickSearchRepository>();
            services.AddSingleton<ICompanyRepository, QuickSearchRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITagRepository, QuickSearchRepository>();
            services.AddSingleton<IStatisticsRepository, StatisticsRepository>();

            return services;
        }
    }
}
