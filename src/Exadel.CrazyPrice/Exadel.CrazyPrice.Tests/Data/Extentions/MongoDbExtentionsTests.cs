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
            IServiceCollection _service = new ServiceCollection();
            _service.AddMongoDb();
            var mainService = _service.FirstOrDefault(d => d.ImplementationType == typeof(MongoDbConfiguration));
            mainService.Should().NotBeNull();
        }
    }
}
