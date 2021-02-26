using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Data.Seeder.Extentions;
using Exadel.CrazyPrice.Data.Seeder.Models.Option;
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

        public string Path { get; set; }

        public DestinationOption Destination { get; set; }

        public bool ClearDataBeforeSeed { get; set; }

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
            Database = configuration.ToStringWithValue("Database:ConnectionStrings:Name")
                .OverLoadString(seederConfiguration.Database, args, "-m", "--mongoName");
            Path = configuration.ToStringWithValue("Files:Path", "", false)
                .OverLoadString(seederConfiguration.Path, args, "-p", "--path");

            TimeReportSec = configuration.ToUint("TimeReportSec")
                .OverLoadUint(seederConfiguration.TimeReportSec, args, "-t", "--time");
            DefaultCountSeed = configuration.ToUint("DefaultCountSeed")
                .OverLoadUint(seederConfiguration.DefaultCountSeed, args, "-n", "--number");

            RewriteIndexes = configuration.ToBool("Database:RewriteIndexes");
            HideDetailsInfo = configuration.ToBool("HideDetailsInfo");
            ClearDataBeforeSeed = configuration.ToBool("ClearDataBeforeSeed");
            CreateTags = configuration.ToBool("CreateTags");
            CreateUsers = configuration.ToBool("CreateUsers");

            Destination = configuration.ToStringWithValue("Destination")
                .OverLoadString(seederConfiguration.Destination.ToString(), args, "-d", "--destination").ToDestinationOption(DestinationOption.mg, true);

            if (!args.Any(a => a.Length < 20 && a.Contains("-")))
            {
                return;
            }

            HideDetailsInfo = HideDetailsInfo.OverLoadBool(seederConfiguration.HideDetailsInfo, args, "-h", "--hide");
            RewriteIndexes = RewriteIndexes.OverLoadBool(seederConfiguration.RewriteIndexes, args, "-r", "--rewrite");
            ClearDataBeforeSeed = ClearDataBeforeSeed.OverLoadBool(seederConfiguration.ClearDataBeforeSeed, args, "-c", "--clear");
            CreateTags = CreateTags.OverLoadBool(seederConfiguration.CreateTags, args, "--tags");
            CreateUsers = CreateUsers.OverLoadBool(seederConfiguration.CreateUsers, args, "--users");
        }
    }
}