using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder
{
    /// <summary>
    /// Represents the seed manager.
    /// </summary>
    public class SeedManager
    {
        private readonly SeederConfiguration _configuration;

        public SeedManager(SeederConfiguration configuration)
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

            var collections = new List<ISeed>
            {
                new DbDiscountSeed(_configuration),
                new DbTagSeed(_configuration),
                new DbUserSeed(_configuration)
            };

            var status = new StringBuilder();
            foreach (var value in collections)
            {
                await value.DeleteIfSetAsync();
                await value.CreateIndexesAsync();
                await value.SeedAsync();
                var currentStatus = await value.StatusTotalAsync();
                status.Append(currentStatus + Environment.NewLine);
            }

            totalWatch.Stop();

            var stopTime = DateTime.Now;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Finished." + Environment.NewLine +
                              status +
                              $"Start time: {startTime}" + Environment.NewLine +
                              $"End time: {stopTime}" + Environment.NewLine +
                              $"TotalSeconds: {totalWatch.Elapsed.TotalSeconds:0.000000}");
            Console.WriteLine("Press any key to Exit.");
        }
    }
}
