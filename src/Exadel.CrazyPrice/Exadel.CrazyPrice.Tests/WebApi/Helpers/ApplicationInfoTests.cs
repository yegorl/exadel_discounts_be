using Exadel.CrazyPrice.WebApi.Helpers;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Helpers
{
    public class ApplicationInfoTests
    {
        [Fact]
        public void ApplicationInfoTest()
        {
            ApplicationInfo.ApplicationName.Should().Be("CrazyPrice.WebApi");
        }
    }
}
