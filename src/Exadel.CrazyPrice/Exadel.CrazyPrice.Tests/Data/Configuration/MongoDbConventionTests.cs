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
    public class MongoDbConventionTests
    {
        [Fact]
        public void MongoDbConventionTest()
        {
            Action action = () => MongoDbConvention.SetCamelCaseElementNameConvention();
            action.Should().NotThrow();
        }
    }
}
