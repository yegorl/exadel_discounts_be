using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Exadel.CrazyPrice.Data.Seeder.Configuration
{
    public class SeederConfiguration
    {
        public SeederConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            string GetSection(string s) => configuration.GetSection($"Database:{s}").Value;

            ConnectionString = GetSection("ConnectionStrings:DefaultConnection").ToStringWithValue();
            Database = GetSection("ConnectionStrings:Database").ToStringWithValue();

            DetailsInfo = GetSection("DetailsInfo").ToBool();
            RewriteIndexes = GetSection("RewriteIndexes").ToBool();
            ClearDatabaseBeforeSeed = GetSection("ClearDatabaseBeforeSeed").ToBool();
            CreateTags = GetSection("CreateTags").ToBool();
            CreateUsers = GetSection("CreateUsers").ToBool();

            TimeReportSec = GetSection("TimeReportSec").ToUint();
            DefaultCountSeed = GetSection("DefaultCountSeed").ToUint();
        }

        public bool DetailsInfo { get; set; }

        public bool CreateTags { get; set; }

        public bool CreateUsers { get; set; }

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

            bool KeysExist(params string[] keys) => keys.Any(k => argsNormalize.Contains(k));

            ConnectionString = KeysExist("-s", "--connectionStrings") ? seederConfiguration.ConnectionString : ConnectionString;
            Database = KeysExist("-d", "--database") ? seederConfiguration.Database : Database;

            DetailsInfo = KeysExist("-i", "--info") ? seederConfiguration.DetailsInfo : DetailsInfo;
            RewriteIndexes = KeysExist("-r", "--rewrite") ? seederConfiguration.RewriteIndexes : RewriteIndexes;
            ClearDatabaseBeforeSeed = KeysExist("-c", "--clear") ? seederConfiguration.ClearDatabaseBeforeSeed : ClearDatabaseBeforeSeed;
            CreateTags = KeysExist("--tags") ? seederConfiguration.CreateTags : CreateTags;
            CreateUsers = KeysExist("--users") ? seederConfiguration.CreateUsers : CreateUsers;

            TimeReportSec = KeysExist("-t", "--time") ? seederConfiguration.TimeReportSec : TimeReportSec;
            DefaultCountSeed = KeysExist("-n", "--number") ? seederConfiguration.DefaultCountSeed : DefaultCountSeed;
        }
    }
}