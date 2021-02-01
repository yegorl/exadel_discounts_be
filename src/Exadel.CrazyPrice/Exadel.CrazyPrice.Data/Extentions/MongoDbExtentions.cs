using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Context;
using Exadel.CrazyPrice.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Exadel.CrazyPrice.Data.Initial;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;

namespace Exadel.CrazyPrice.Data.Extentions
{
    public static class MongoDbExtentions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            ConventionRegistry.Register("camelCase", new ConventionPack
            {
                new CamelCaseElementNameConvention()
            }, t => true);

            AddMongoDbServices(services);
            services.Configure<MongoDbConfiguration>(
                dbConfiguration =>
                {
                    dbConfiguration.ConnectionString =
                        configuration.GetSection("Database:ConnectionStrings:DefaultConnection").Value;

                    dbConfiguration.Database =
                        configuration.GetSection("Database:ConnectionStrings:Database").Value;
                    
                    dbConfiguration.Seed =
                        bool.Parse(configuration.GetSection("Database:Seed").Value);
                });

            return services;
        }

        public static IApplicationBuilder UseMongoDb(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbConfiguration = scope.ServiceProvider.GetService<IOptions<MongoDbConfiguration>>();

            if (dbConfiguration == null)
            {
                throw new ArgumentException(nameof(dbConfiguration));
            }

            if (dbConfiguration.Value.Seed == false) return app;

            var dbContext = scope.ServiceProvider.GetService<IDbContext>();

            if (dbContext == null)
            {
                throw new ArgumentException(nameof(dbContext));
            }

            var seeder = new DbSeeder(dbContext);
            seeder.Seed();

            return app;
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
