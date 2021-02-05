﻿using System;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using McMaster.Extensions.CommandLineUtils;

namespace Exadel.CrazyPrice.Data.Seeder
{
    [Command(Name = "DbSeeder.exe", Description = "Exadel CrazyPrice Database Seeder")]
    [HelpOption("-?")]
    public partial class Program
    {
        [Option("-s|--connectionStrings", Description = "MongoDb connection strings.")]
        private string ConnectionString { get; }

        [Option("-d|--database", Description = "The database in MongoDb.")]
        private string Database { get; }

        [Option("-c|--clear", Description = "Clear the database before seed.")]
        private bool ClearDatabaseBeforeSeed { get; }

        [Option("-r|--rewrite", Description = "Rewrite indexes in the database.")]
        private bool RewriteIndexes { get; }

        [Option("-n|--number", Description = "The default count of documents that seed in the database.")]
        private uint DefaultCountSeed { get; }

        [Option("-i|--info", Description = "Show details info.")]
        private bool DetailsInfo { get; }

        [Option("-t|--time", Description = "Time in sec for report.")]
        private uint TimeReportSec { get; }

        [Option("--tags", Description = "Create tags from discounts and indexes it.")]
        private bool CreateTags { get; }

        private void DetailsInfoMongoDbConfiguration(SeederConfiguration seederConfiguration)
        {
            Console.WriteLine($"ConnectionString: {seederConfiguration.ConnectionString}");
            Console.WriteLine($"Database: {seederConfiguration.Database}");
            Console.WriteLine($"ClearDatabaseBeforeSeed: {seederConfiguration.ClearDatabaseBeforeSeed}");
            Console.WriteLine($"RewriteIndexes: {seederConfiguration.RewriteIndexes}");
            Console.WriteLine($"DefaultCountSeed: {seederConfiguration.DefaultCountSeed}");
            Console.WriteLine($"TimeReportSec: {seederConfiguration.TimeReportSec}");
            Console.WriteLine($"CreateTags: {seederConfiguration.CreateTags}");
        }
    }
}
