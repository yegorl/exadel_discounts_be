using Exadel.CrazyPrice.Data.Seeder.Configuration;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder
{
    public partial class Program
    {
        private static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            SeederConfiguration seederConfiguration;
            try
            {
                seederConfiguration = new SeederConfiguration().Default;
                seederConfiguration.Configure(c =>
                {
                    c.DetailsInfo = DetailsInfo;

                    c.ConnectionString = ConnectionString;
                    c.Database = Database;
                    c.ClearDatabaseBeforeSeed = ClearDatabaseBeforeSeed;
                    c.RewriteIndexes = RewriteIndexes;
                    c.DefaultCountSeed = DefaultCountSeed;
                    c.TimeReportSec = TimeReportSec;
                    c.CreateTags = CreateTags;
                    c.CreateUsers = CreateUsers;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error configuration file: {e.Message}");
                return;
            }

            if (seederConfiguration.DetailsInfo)
            {
                Console.WriteLine(Environment.NewLine);
                DetailsInfoMongoDbConfiguration(seederConfiguration);
            }
            
            Console.WriteLine(new string('*', 30));
           
            Task.Run(async () =>
            {
                var seeder = new SeedManager(seederConfiguration);
                await seeder.Seed();
            });

            Console.WriteLine("Press any key to Abort.");
            Console.WriteLine(new string('*', 30));

            Console.ReadKey();
        }
    }
}
