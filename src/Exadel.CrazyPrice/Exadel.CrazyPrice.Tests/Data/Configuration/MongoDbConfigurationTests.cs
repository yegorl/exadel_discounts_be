using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Data.Configuration;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Data.Configuration
{
    public class MongoDbConfigurationTests
    {
        [Fact]
        public void MongoDbConfigurationTest()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[]
                {
                    new("Database:ConnectionStrings:DefaultConnection", "DefaultConnection"),
                    new("Database:ConnectionStrings:Database","Database")
                })
                .Build();

            var mongoDbConfig = new MongoDbConfiguration(config);

            mongoDbConfig.ConnectionString.Should().NotBeNullOrEmpty();
            mongoDbConfig.Database.Should().NotBeNullOrEmpty();
        }
    }
}
