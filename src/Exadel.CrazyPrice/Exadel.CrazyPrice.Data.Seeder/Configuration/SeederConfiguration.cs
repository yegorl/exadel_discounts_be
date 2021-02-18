using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Exadel.CrazyPrice.Data.Seeder.Configuration
{
    /// <summary>
    /// Determines the seeder configuration.
    /// </summary>
    public class SeederConfiguration
    {
        public bool HideDetailsInfo { get; set; }

        public bool CreateTags { get; set; }

        public bool CreateUsers { get; set; }

        public string ConnectionString { get; set; }

        public string Database { get; set; }

        public bool ClearDatabaseBeforeSeed { get; set; }

        public bool RewriteIndexes { get; set; }

        public uint DefaultCountSeed { get; set; }

        public uint TimeReportSec { get; set; }

        public void Configure(Action<SeederConfiguration, ConfigurationBuilder> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var seederConfiguration = new SeederConfiguration();
            var builder = new ConfigurationBuilder();

            action.Invoke(seederConfiguration, builder);

            if (builder.Sources.Count == 0)
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                builder
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                    .AddEnvironmentVariables();
            }

            var configuration = builder.Build();

            var args = Environment.GetCommandLineArgs();

            ConnectionString = configuration.ToStringWithValue("Database:ConnectionStrings:DefaultConnection")
                .OverLoadString(seederConfiguration.ConnectionString, args, "-s", "--connectionStrings");
            Database = configuration.ToStringWithValue("Database:ConnectionStrings:Database")
                .OverLoadString(seederConfiguration.Database, args, "-d", "--database");

            TimeReportSec = configuration.ToUint("Database:TimeReportSec")
                .OverLoadUint(seederConfiguration.TimeReportSec, args, "-t", "--time");
            DefaultCountSeed = configuration.ToUint("Database:DefaultCountSeed")
                .OverLoadUint(seederConfiguration.DefaultCountSeed, args, "-n", "--number");

            HideDetailsInfo = configuration.ToBool("Database:HideDetailsInfo");
            RewriteIndexes = configuration.ToBool("Database:RewriteIndexes");
            ClearDatabaseBeforeSeed = configuration.ToBool("Database:ClearDatabaseBeforeSeed");
            CreateTags = configuration.ToBool("Database:CreateTags");
            CreateUsers = configuration.ToBool("Database:CreateUsers");

            if (!args.Any(a => a.Length < 20 && a.Contains("-"))) return;

            HideDetailsInfo = HideDetailsInfo.OverLoadBool(seederConfiguration.HideDetailsInfo, args, "-h", "--hide");
            RewriteIndexes = RewriteIndexes.OverLoadBool(seederConfiguration.RewriteIndexes, args, "-r", "--rewrite");
            ClearDatabaseBeforeSeed = ClearDatabaseBeforeSeed.OverLoadBool(seederConfiguration.ClearDatabaseBeforeSeed, args, "-c", "--clear");
            CreateTags = CreateTags.OverLoadBool(seederConfiguration.CreateTags, args, "--tags");
            CreateUsers = CreateUsers.OverLoadBool(seederConfiguration.CreateUsers, args, "--users");
        }
    }
}