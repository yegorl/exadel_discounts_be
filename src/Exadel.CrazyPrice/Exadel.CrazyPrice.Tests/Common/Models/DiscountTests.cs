using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Promocode;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models
{
    public class DiscountTests
    {
        [Fact]
        public void DiscountTest()
        {
            var discount = new Discount()
            {
                FavoritesUsersId = new List<string>(),
                UsersPromocodes = new List<UserPromocodes>(),
                RatingTotal = 1,
                ViewsTotal = 1,
                SubscriptionsTotal = 1,
                Deleted = true
            };

            discount.FavoritesUsersId.Should().NotBeNull();
            discount.UsersPromocodes.Should().NotBeNull();
            discount.RatingTotal.Should().Be(1);
            discount.ViewsTotal.Should().Be(1);
            discount.SubscriptionsTotal.Should().Be(1);
            discount.Deleted.Should().BeTrue();
        }
    }
}
