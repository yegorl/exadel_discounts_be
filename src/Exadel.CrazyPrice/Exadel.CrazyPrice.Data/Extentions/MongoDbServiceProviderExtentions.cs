using Exadel.CrazyPrice.Common.Configurations;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Context;
using Exadel.CrazyPrice.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Exadel.CrazyPrice.Data.Extentions
{
    public static class MongoDbServiceProviderExtentions
    {
        public static void AddMongoDb(this IServiceCollection services, Action<MongoDbConfiguration> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            AddMongoDbServices(services);
            services.Configure(setupAction);
        }

        private static void AddMongoDbServices(IServiceCollection services)
        {
            services.AddSingleton<IDbConfiguration, MongoDbConfiguration>();
            services.AddSingleton<IDbContext, MongoDbContext>();
            services.AddSingleton<IAddressRepository, AddressRepository>();
            services.AddSingleton<ICompanyRepository, CompanyRepository>();
            services.AddSingleton<IDiscountRepository, DiscountRepository>();
            services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddSingleton<ITagRepository, TagRepository>();
        }
    }
}
