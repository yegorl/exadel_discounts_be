using Exadel.CrazyPrice.Data.Seeder.Extentions;
using Microsoft.Extensions.Configuration;
using System;

namespace Exadel.CrazyPrice.Data.Seeder.Configuration
{
    /// <summary>
    /// Determines the seeder configuration.
    /// </summary>
    public class SeederConfiguration
    {
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

            var args = Environment.GetCommandLineArgs();

            ConnectionString = ConnectionString.OverLoadString(seederConfiguration.ConnectionString, args, "-s", "--connectionStrings");
            Database = Database.OverLoadString(seederConfiguration.Database, args, "-d", "--database");

            DetailsInfo = DetailsInfo.OverLoadBool(seederConfiguration.DetailsInfo, args, "-i", "--info");
            RewriteIndexes = RewriteIndexes.OverLoadBool(seederConfiguration.RewriteIndexes, args, "-r", "--rewrite");
            ClearDatabaseBeforeSeed = ClearDatabaseBeforeSeed.OverLoadBool(seederConfiguration.ClearDatabaseBeforeSeed, args, "-c", "--clear");
            CreateTags = CreateTags.OverLoadBool(seederConfiguration.CreateTags, args, "--tags");
            CreateUsers = CreateUsers.OverLoadBool(seederConfiguration.CreateUsers, args, "--users");

            TimeReportSec = TimeReportSec.OverLoadUint(seederConfiguration.TimeReportSec, args, "-t", "--time");
            DefaultCountSeed = DefaultCountSeed.OverLoadUint(seederConfiguration.DefaultCountSeed, args, "-n", "--number");
        }

        /// <summary>
        /// Gets default seeder configuration.
        /// </summary>
        public SeederConfiguration Default
        {
            get
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                    .AddEnvironmentVariables()
                    .Build();

                ConnectionString = configuration.ToStringWithValue("Database:ConnectionStrings:DefaultConnection");
                Database = configuration.ToStringWithValue("Database:ConnectionStrings:Database");

                DetailsInfo = configuration.ToBool("Database:DetailsInfo");
                RewriteIndexes = configuration.ToBool("Database:RewriteIndexes");
                ClearDatabaseBeforeSeed = configuration.ToBool("Database:ClearDatabaseBeforeSeed");
                CreateTags = configuration.ToBool("Database:CreateTags");
                CreateUsers = configuration.ToBool("Database:CreateUsers");

                TimeReportSec = configuration.ToUint("Database:TimeReportSec");
                DefaultCountSeed = configuration.ToUint("Database:DefaultCountSeed");

                return this;
            }
        }
    }
}