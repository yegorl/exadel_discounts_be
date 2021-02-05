using Exadel.CrazyPrice.Data.Indexes;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using MongoDB.Driver;
using System;
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
            var discountData = new FakeDiscounts().Get(count);
            await Collection.InsertManyAsync(discountData);
        }
    }
}