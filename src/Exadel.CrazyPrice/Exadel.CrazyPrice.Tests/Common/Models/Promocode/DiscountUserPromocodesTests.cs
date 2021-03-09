using System;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Promocode;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models.Promocode
{
    public class DiscountUserPromocodesTests
    {
        [Fact]
        public void DiscountUserPromocodesTest()
        {
            var discountUserPromocodes = new DiscountUserPromocodes()
            {
                DiscountId = Guid.Parse("cf79c9c1-b0b0-4577-82e0-96e6245b4629"),
                UserPromocodes = new UserPromocodes(),
                Company = new Company(),
                CurrentPromocode = new CrazyPrice.Common.Models.Promocode.Promocode(),
                DiscountName = ""
            };

            discountUserPromocodes.DiscountId.Should().NotBeEmpty();
            discountUserPromocodes.UserPromocodes.Should().NotBeNull();
            discountUserPromocodes.Company.Should().NotBeNull();
            discountUserPromocodes.CurrentPromocode.Should().NotBeNull();
            discountUserPromocodes.DiscountName.Should().NotBeNull();
        }
    }
}
