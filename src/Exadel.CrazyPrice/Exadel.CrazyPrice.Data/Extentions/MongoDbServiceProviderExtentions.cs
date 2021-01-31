using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Context;
using Exadel.CrazyPrice.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Exadel.CrazyPrice.Data.Extentions
{
    public static class MongoDbServiceProviderExtentions
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            AddMongoDbServices(services);
            services.Configure<MongoDbConfiguration>(
                dbConfiguration =>
                {
                    dbConfiguration.ConnectionString =
                        configuration.GetSection("ConnectionStrings:DefaultConnection").Value;

                    dbConfiguration.Database =
                        configuration.GetSection("ConnectionStrings:Database").Value;
                });
        }

        private static void AddMongoDbServices(IServiceCollection services)
        {
            services.AddSingleton<IDbContext, MongoDbContext>();
            services.AddSingleton<IAddressRepository, AddressRepository>();
            services.AddSingleton<ICompanyRepository, CompanyRepository>();
            services.AddSingleton<IDiscountRepository, DiscountRepository>();
            services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddSingleton<ITagRepository, TagRepository>();
        }
    }
}
