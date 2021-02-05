using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder
{
    public class DbSeederManager
    {
        private readonly SeederConfiguration _configuration;

        public DbSeederManager(SeederConfiguration configuration)
        {
            MongoDbConvention.SetCamelCaseElementNameConvention();
            _configuration = configuration;
        }

        public async Task Seed()
        {
            var totalWatch = new Stopwatch();

            var startTime = DateTime.Now;
            Console.WriteLine($"Seed start: {startTime}");

            totalWatch.Start();

            var dbDiscounts = new DbDiscountSeed(_configuration);
            await dbDiscounts.CreateIndexesAsync();
            await dbDiscounts.SeedAsync();

            var dbTags = new DbTagSeed(_configuration);
            await dbTags.CreateIndexesAsync();
            await dbTags.SeedAsync();

            totalWatch.Stop();

            var discountsStatus = await dbDiscounts.StatusTotalAsync();
            var tagStatus = await dbTags.StatusTotalAsync();

            var stopTime = DateTime.Now;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Finished." + Environment.NewLine +
                              discountsStatus + Environment.NewLine +
                              tagStatus + Environment.NewLine +
                              $"Start time: {startTime}" + Environment.NewLine +
                              $"End time: {stopTime}" + Environment.NewLine +
                              $"TotalSeconds: {totalWatch.Elapsed.TotalSeconds:0.000000}");
            Console.WriteLine("Press any key to Exit.");
        }
    }
}
