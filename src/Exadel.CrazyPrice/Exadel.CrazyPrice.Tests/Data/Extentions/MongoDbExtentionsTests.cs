using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Extentions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Data.Extentions
{
    public class MongoDbExtentionsTests
    {
        [Fact]
        public void MongoDbExtentionsTest()
        {
            IServiceCollection service = new ServiceCollection();
            service.AddMongoDb();
            var mainService = service.FirstOrDefault(d => d.ImplementationType == typeof(MongoDbConfiguration));
            mainService.Should().NotBeNull();
        }
    }
}
