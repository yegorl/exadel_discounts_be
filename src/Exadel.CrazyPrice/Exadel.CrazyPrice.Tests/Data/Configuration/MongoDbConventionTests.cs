using Exadel.CrazyPrice.Data.Configuration;
using FluentAssertions;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Linq;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Data.Configuration
{
    public class MongoDbConventionTests
    {
        [Fact]
        public void MongoDbConventionTest()
        {
            MongoDbConvention.SetCamelCaseElementNameConvention();
            var convention = ConventionRegistry.Lookup(typeof(Type)).Conventions.FirstOrDefault(c => c.Name == "CamelCaseElementName");
            convention.Should().NotBeNull();
        }
    }
}
