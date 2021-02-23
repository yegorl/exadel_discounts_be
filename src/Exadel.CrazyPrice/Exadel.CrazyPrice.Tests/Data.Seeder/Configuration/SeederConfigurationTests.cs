using Exadel.CrazyPrice.Data.Seeder.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Data.Seeder.Configuration
{
    public class SeederConfigurationTests
    {
        [Fact]
        public void SeederConfigurationTest()
        {
            var seederConfiguration = new SeederConfiguration();
            seederConfiguration.Configure((c, b) =>
            {
                c.ConnectionString = "str";
                c.CreateTags = false;
                c.ClearDatabaseBeforeSeed = false;
                c.CreateUsers = false;
                c.Database = "database";
                c.DefaultCountSeed = 1000;
                c.HideDetailsInfo = false;
                c.RewriteIndexes = false;
                c.TimeReportSec = 2;

                b.AddInMemoryCollection(new KeyValuePair<string, string>[]
                {
                    new("Database:ConnectionStrings:DefaultConnection", "DefaultConnection"),
                    new("Database:ConnectionStrings:Database","Database"),

                    new("Database:HideDetailsInfo","true"),
                    new("Database:RewriteIndexes","true"),
                    new("Database:ClearDatabaseBeforeSeed","true"),
                    new("Database:CreateTags","true"),
                    new("Database:CreateUsers","true"),

                    new("Database:TimeReportSec","1"),
                    new("Database:DefaultCountSeed","5000"),
                });
            });
            seederConfiguration.ConnectionString.Should().BeEquivalentTo("DefaultConnection");
            seederConfiguration.DefaultCountSeed.Should().Be(5000);
        }

        [Fact]
        public void SeederConfigurationRaiseArgumentNullExceptionTest()
        {
            var seederConfiguration = new SeederConfiguration();
            Action action = () => seederConfiguration.Configure(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SeederConfigurationFreeTest()
        {
            var seederConfiguration = new SeederConfiguration();

            Action action = () => seederConfiguration.Configure((c, b) =>
            {
                c.ConnectionString = "str";
                c.CreateTags = false;
                c.ClearDatabaseBeforeSeed = false;
                c.CreateUsers = false;
                c.Database = "database";
                c.DefaultCountSeed = 1000;
                c.HideDetailsInfo = false;
                c.RewriteIndexes = false;
                c.TimeReportSec = 2;
            });
            action.Should().Throw<ArgumentException>();

        }
    }
}
