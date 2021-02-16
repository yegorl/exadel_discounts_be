using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models
{
    public class UserTests
    {
        [Fact]
        public void UserTest()
        {
            var user = new User()
            {
                Roles = RoleOption.Employee
            };

            user.Roles.Should().BeEquivalentTo(RoleOption.Employee);
        }
    }
}
