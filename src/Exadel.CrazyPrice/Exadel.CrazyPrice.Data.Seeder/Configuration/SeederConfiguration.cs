using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Exadel.CrazyPrice.Data.Seeder.Configuration
{
    public class SeederConfiguration
    {
        private readonly IConfiguration _configuration;

        public SeederConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var temp = _configuration.GetSection("Database:ClearDatabaseBeforeSeed").Value;
            if (!bool.TryParse(temp, out var clearDatabaseBeforeSeed))
            {
                throw new ArgumentException("Database:ClearDatabaseBeforeSeed musts be the valid bool value.");
            }

            if (!bool.TryParse(_configuration.GetSection("Database:RewriteIndexes").Value, out var rewriteIndexes))
            {
                throw new ArgumentException("Database:RewriteIndexes musts be the valid bool value.");
            }

            if (!bool.TryParse(_configuration.GetSection("Database:DetailsInfo").Value, out var detailsInfo))
            {
                throw new ArgumentException("Database:DetailsInfo musts be the valid bool value.");
            }

            if (!bool.TryParse(_configuration.GetSection("Database:CreateTags").Value, out var createTags))
            {
                throw new ArgumentException("Database:CreateTags musts be the valid bool value.");
            }

            if (!uint.TryParse(_configuration.GetSection("Database:DefaultCountSeed").Value, out var defaultCountSeed))
            {
                throw new ArgumentException("Database:DefaultCountSeed musts be the valid uint value.");
            }

            if (!uint.TryParse(_configuration.GetSection("Database:TimeReportSec").Value, out var reportEverySec))
            {
                throw new ArgumentException("Database:TimeReportSec musts be the valid uint value.");
            }

            DetailsInfo = detailsInfo;
            TimeReportSec = reportEverySec;
            CreateTags = createTags;

            ConnectionString = _configuration.GetSection("Database:ConnectionStrings:DefaultConnection").Value;
            Database = _configuration.GetSection("Database:ConnectionStrings:Database").Value;
            ClearDatabaseBeforeSeed = clearDatabaseBeforeSeed;
            RewriteIndexes = rewriteIndexes;
            DefaultCountSeed = defaultCountSeed;
        }

        public bool DetailsInfo { get; set; }

        public bool CreateTags { get; set; }

        public string ConnectionString { get; set; }

        public string Database { get; set; }

        public bool ClearDatabaseBeforeSeed { get; set; }

        public bool RewriteIndexes { get; set; }

        public uint DefaultCountSeed { get; set; }

        public uint TimeReportSec { get; set; }

        public void Configure(Action<SeederConfiguration> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var seederConfiguration = new SeederConfiguration();

            action.Invoke(seederConfiguration);

            var argsNormalize = string.Join("(|)", Environment.GetCommandLineArgs()).ToLowerInvariant().Split("(|)");

            if (argsNormalize.Contains("-i") || argsNormalize.Contains("--info"))
            {
                DetailsInfo = seederConfiguration.DetailsInfo;
            }

            if (argsNormalize.Contains("-s") || argsNormalize.Contains("--connectionStrings"))
            {
                ConnectionString = seederConfiguration.ConnectionString;
            }

            if (argsNormalize.Contains("-d") || argsNormalize.Contains("--database"))
            {
                Database = seederConfiguration.Database;
            }

            if (argsNormalize.Contains("-c") || argsNormalize.Contains("--clear"))
            {
                ClearDatabaseBeforeSeed = seederConfiguration.ClearDatabaseBeforeSeed;
            }

            if (argsNormalize.Contains("-n") || argsNormalize.Contains("--number"))
            {
                DefaultCountSeed = seederConfiguration.DefaultCountSeed;
            }

            if (argsNormalize.Contains("-r") || argsNormalize.Contains("--rewrite"))
            {
                RewriteIndexes = seederConfiguration.RewriteIndexes;
            }

            if (argsNormalize.Contains("-t") || argsNormalize.Contains("--time"))
            {
                TimeReportSec = seederConfiguration.TimeReportSec;
            }

            if (argsNormalize.Contains("--tags"))
            {
                CreateTags = seederConfiguration.CreateTags;
            }
        }
    }
}