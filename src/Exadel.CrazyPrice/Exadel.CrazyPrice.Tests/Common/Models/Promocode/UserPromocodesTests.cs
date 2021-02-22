using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Promocode;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models.Promocode
{
    public class UserPromocodesTests
    {
        [Fact]
        public void UserPromocodesTest()
        {
            var userPromocodes = new UserPromocodes()
            {
                UserId = Guid.Parse("675f0949-fd50-4738-92e2-9523ecc031d1"),
                Promocodes = new List<CrazyPrice.Common.Models.Promocode.Promocode>()
                {
                    new()
                    {
                        Id = Guid.Parse("dc666524-36a3-43ef-998c-7f250793d9bc"),
                        CreateDate = DateTime.UtcNow,
                        EndDate = DateTime.UtcNow,
                        Deleted = false,
                        PromocodeValue = StringExtentions.NewPromocodeValue()
                    }
                }
            };

            userPromocodes.UserId.Should().NotBeEmpty();
            userPromocodes.Promocodes.Should().NotBeNull();
        }
    }
}
