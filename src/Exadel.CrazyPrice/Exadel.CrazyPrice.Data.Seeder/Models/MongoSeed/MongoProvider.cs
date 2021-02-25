using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models.MongoSeed
{
    public sealed class MongoProvider : DataProvider
    {
        public MongoProvider(SeederConfiguration configuration)
        {
            MongoDbConvention.SetCamelCaseElementNameConvention();
            Collections = new List<ISeed>
            {
                new MongoDiscountSeed(configuration),
                new MongoTagSeed(configuration),
                new MongoUserSeed(configuration)
            };
        }

        protected override async Task Work()
        {
            var status = new StringBuilder();
            foreach (var value in Collections)
            {
                await value.DeleteIfSetAsync();
                await value.SeedAsync();
                Console.WriteLine(new string('*', 10));
                var currentStatus = await value.StatusTotalAsync();
                status.Append(currentStatus + Environment.NewLine);
            }

            Status = status.ToString();
        }
    }
}
