using Exadel.CrazyPrice.Data.Seeder.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Models;
using Exadel.CrazyPrice.Data.Seeder.Models.FileSeed;
using Exadel.CrazyPrice.Data.Seeder.Models.MongoSeed;
using Exadel.CrazyPrice.Data.Seeder.Models.Option;
using System;
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
            _configuration = configuration;
        }

        public async Task Seed(StateExecutionConfiguration stateExecutionConfiguration)
        {
            IDataProvider provider;

            if (_configuration.Destination == DestinationOption.mg)
            {
                provider = new MongoProvider(_configuration);
            }
            else
            {
                provider = new FileProvider(_configuration);
            }

            if (provider.ActionWhenAborted != null)
            {
                stateExecutionConfiguration.ExecutionActionsAfterAbort = provider.ActionWhenAborted;
            }

            Console.WriteLine($"Seed start: {DateTime.Now}");

            await provider.WriteAsync();

            Console.WriteLine("Press any key to Exit.");
        }
    }
}
