using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models
{
    public class UserTests
    {
        [Theory]
        [InlineData(RoleOption.Unknown, RoleOption.Unknown)]
        [InlineData(RoleOption.Employee, RoleOption.Employee)]
        [InlineData(RoleOption.Moderator, RoleOption.Moderator)]
        [InlineData(RoleOption.Administrator, RoleOption.Administrator)]
        public void UserTest(RoleOption roleOptionIn, RoleOption roleOptionOut)
        {
            var user = new User()
            {
                Roles = roleOptionIn
            };

            user.Roles.Should().BeEquivalentTo(roleOptionOut);
        }
    }
}
