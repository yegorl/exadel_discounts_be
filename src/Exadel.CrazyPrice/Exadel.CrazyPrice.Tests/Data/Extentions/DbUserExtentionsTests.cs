using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Data.Extentions
{
    public class DbUserExtentionsTests
    {
        [Fact]
        public void GetOneFreeDbUserTest()
        {
            var users = new List<DbUser>();
            users.GetOne().Should().BeEquivalentTo(new DbUser());
        }

        [Fact]
        public void ToUserTest()
        {
            var user = new DbUser();
            user.ToUser().Should().BeEquivalentTo(new User());
        }

        [Fact]
        public void ToDbUserTest()
        {
            var user = new User()
            {
                Id = Guid.Parse("2309125b-98e6-49fa-bb6e-2e9dd6e9e525"),
                Name = "Name",
                Mail = "mail@tut.com",
                PhoneNumber = "375278964517",
                Roles = RoleOption.Employee,
                Surname = "Surname",
                HashPassword = "HashPassword",
                Salt = "Salt"
            };
            user.ToDbUser().Should().BeEquivalentTo(new DbUser()
            {
                Id = "2309125b-98e6-49fa-bb6e-2e9dd6e9e525",
                Name = "Name",
                Mail = "mail@tut.com",
                PhoneNumber = "375278964517",
                Roles = RoleOption.Employee,
                Surname = "Surname",
                HashPassword = "HashPassword",
                Salt = "Salt"
            });
        }
    }
}
