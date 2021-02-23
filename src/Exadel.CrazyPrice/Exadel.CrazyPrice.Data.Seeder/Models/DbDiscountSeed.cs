using Exadel.CrazyPrice.Data.Indexes;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models
{
    public class DbDiscountSeed : AbstractSeed<DbDiscount>
    {
        public DbDiscountSeed(SeederConfiguration configuration) : base(configuration)
        {
            CollectionName = "Discounts";
            IndexModels = DbDiscountIndexes.GetIndexes;
            DefaultCountSeed = configuration.DefaultCountSeed;

            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);

            Collection = db.GetCollection<DbDiscount>(CollectionName);
        }

        public override async Task SeedAsync()
        {
            uint countForInsert = 500;

            if (DefaultCountSeed == 0)
            {
                return;
            }

            var count = Math.Truncate((double)DefaultCountSeed / countForInsert);
            var countPart = DefaultCountSeed - (uint)count * countForInsert;

            await base.SeedAsync();

            if (DefaultCountSeed > countForInsert)
            {
                for (var i = 0; i < count; i++)
                {
                    await InsertAsync(countForInsert);
                }

                if (countPart > 0)
                {
                    await InsertAsync(countPart);
                }
            }
            else
            {
                await InsertAsync(DefaultCountSeed);
            }

            await TimerDisposeAsync();
        }

        private async Task InsertAsync(uint count)
        {
            var geoCountries = FakeData.GeoCountries;

            if (count < geoCountries.Count)
            {
                geoCountries = geoCountries.GetRange(0, Convert.ToInt32(count));
            }

            var iteration = Math.Truncate((double)count / geoCountries.Count);

            var rest = count - iteration * geoCountries.Count;

            foreach (var geoCountry in geoCountries)
            {
                await InsertAsync(Convert.ToUInt32(iteration), FakeData.UsersId, geoCountry);
            }

            if (rest > 0)
            {
                await InsertAsync(Convert.ToUInt32(rest), FakeData.UsersId, geoCountries[0]);
            }
        }

        private async Task InsertAsync(uint count, List<string> usersId, GeoCountry geo)
        {
            var discountData = new FakeDiscounts().Get(count, usersId, geo);
            await Collection.InsertManyAsync(discountData);
        }
    }
}