using Exadel.CrazyPrice.Data.Seeder.Configuration;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder
{
    public partial class Program
    {
        private StateExecutionConfiguration _stateExecutionConfiguration;

        private static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            SeederConfiguration seederConfiguration;
            try
            {
                seederConfiguration = new SeederConfiguration();
                seederConfiguration.Configure((c, b) =>
                {
                    c.HideDetailsInfo = HideDetailsInfo;

                    c.ConnectionString = ConnectionString;
                    c.Database = Database;
                    c.ClearDataBeforeSeed = ClearDataBeforeSeed;
                    c.RewriteIndexes = RewriteIndexes;
                    c.DefaultCountSeed = DefaultCountSeed;
                    c.TimeReportSec = TimeReportSec;
                    c.CreateTags = CreateTags;
                    c.CreateUsers = CreateUsers;
                    c.Path = Path;
                    c.Destination = Destination;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error configuration file: {e.Message}");
                return;
            }

            if (!seederConfiguration.HideDetailsInfo)
            {
                Console.WriteLine(Environment.NewLine);
                DetailsInfoConfiguration(seederConfiguration);
            }

            Console.WriteLine(new string('*', 30));

            _stateExecutionConfiguration = new StateExecutionConfiguration();
            Task.Run(async () =>
            {
                var seeder = new SeedManager(seederConfiguration);
                await seeder.Seed(_stateExecutionConfiguration);
                _stateExecutionConfiguration.Success();
            });

            Console.WriteLine("Press any key to Abort.");
            Console.WriteLine(new string('*', 30));

            Console.ReadKey();

            if (!_stateExecutionConfiguration.Aborted)
            {
                return;
            }

            Console.WriteLine("Execution aborted. Please wait.");
            
            if (_stateExecutionConfiguration.ExecutionActionsAfterAbort == null) return;
            foreach (var action in _stateExecutionConfiguration.ExecutionActionsAfterAbort.GetInvocationList())
            {
                action.DynamicInvoke();
            }
        }
    }
}
