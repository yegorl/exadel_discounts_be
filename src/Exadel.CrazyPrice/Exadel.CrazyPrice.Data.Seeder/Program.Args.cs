using Exadel.CrazyPrice.Data.Seeder.Configuration;
using McMaster.Extensions.CommandLineUtils;
using System;
using Exadel.CrazyPrice.Data.Seeder.Models.Option;

namespace Exadel.CrazyPrice.Data.Seeder
{
    [Command(Name = "DbSeeder.exe", Description = "Exadel CrazyPrice Database Seeder")]
    [HelpOption("-?")]
    public partial class Program
    {
        [Option("-s|--connectionStrings", Description = "MongoDb connection strings.")]
        private string ConnectionString { get; }

        [Option("-m|--mongoName", Description = "The database name in MongoDb.")]
        private string Database { get; }

        [Option("-c|--clear", Description = "Clear the data before seed.")]
        private bool ClearDataBeforeSeed { get; }

        [Option("-r|--rewrite", Description = "Rewrite indexes in the database.")]
        private bool RewriteIndexes { get; }

        [Option("-n|--number", Description = "The default count of documents that seed.")]
        private uint DefaultCountSeed { get; }

        [Option("-h|--hide", Description = "Hide details info.")]
        private bool HideDetailsInfo { get; }

        [Option("-t|--time", Description = "Time in sec for report.")]
        private uint TimeReportSec { get; }
        
        [Option("-p|--path", Description = "Path for save files.")]
        private string Path { get; }

        [Option("-d|--destination", Description = "Destination for save data: [mg] - mongo or [fs] - files.")]
        private DestinationOption Destination { get; }

        [Option("--tags", Description = "Create tags from discounts and indexes it.")]
        private bool CreateTags { get; }

        [Option("--users", Description = "Create users and indexes it.")]
        private bool CreateUsers { get; }

        private void DetailsInfoConfiguration(SeederConfiguration seederConfiguration)
        {
            Console.WriteLine($"ConnectionString: {seederConfiguration.ConnectionString}");
            Console.WriteLine($"Database: {seederConfiguration.Database}");
            Console.WriteLine($"Path: {seederConfiguration.Path}");
            Console.WriteLine($"Destination: {seederConfiguration.Destination}");
            Console.WriteLine($"ClearDataBeforeSeed: {seederConfiguration.ClearDataBeforeSeed}");
            Console.WriteLine($"RewriteIndexes: {seederConfiguration.RewriteIndexes}");
            Console.WriteLine($"DefaultCountSeed: {seederConfiguration.DefaultCountSeed}");
            Console.WriteLine($"TimeReportSec: {seederConfiguration.TimeReportSec}");
            Console.WriteLine($"CreateTags: {seederConfiguration.CreateTags}");
            Console.WriteLine($"CreateUsers: {seederConfiguration.CreateUsers}");
        }
    }
}
