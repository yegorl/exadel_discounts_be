using Exadel.CrazyPrice.Data.Seeder.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models.FileSeed
{
    public sealed class FileProvider : DataProvider
    {
        private readonly SeederConfiguration _configuration;

        public FileProvider(SeederConfiguration configuration)
        {
            _configuration = configuration;
            Collections = new List<ISeed>
            {
                new FileDiscountSeed(configuration),
                new FileTagSeed(configuration),
                new FileUserSeed(configuration),
                new FileExternalUserSeed(configuration)
            };
            ActionWhenAborted += () =>
            {
                var fullName = Path.Combine(configuration.Path, "Discounts.json");
                //for (var numTries = 0; numTries < 10; numTries++)
                while (true)
                {
                    try
                    {
                        FileDiscountSeed.ChangeSymbol(fullName);
                        Thread.Sleep(50);
                        break;
                    }
                    catch (IOException)
                    {
                        Thread.Sleep(150);
                    }
                }
                
                Console.WriteLine("Discounts seed aborted.");
            };
        }

        protected override async Task Work()
        {
            var status = new StringBuilder();
            foreach (var value in Collections)
            {
                await value.DeleteIfSetAsync();
                await value.SeedAsync();
                var currentStatus = await value.StatusTotalAsync();
                status.Append(currentStatus + Environment.NewLine);
            }
            if (!Directory.Exists(_configuration.Path))
            {
                status.Append("Directory is not exists.\n\r");
                Status = status.ToString();
                return;
            }


            status.Append("Hello");

            Status = status.ToString();
        }
    }
}
