using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Response;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models
{
    public class DiscountResponseTests
    {
        [Fact]
        public void DiscountResponseTest()
        {
            var discountResponse = new DiscountResponse()
            {
                Id = new Guid("3ec66999-6ca6-4c75-b6c8-999d018ec670"),
                Name = "Name",
                Description = "Description",
                AmountOfDiscount = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Address = new Address(),
                Company = new Company(),
                WorkingDaysOfTheWeek = "0101011",
                Tags = new List<string>(),
                RatingTotal = 1,
                ViewsTotal = 1,
                SubscriptionsTotal = 1,
                CreateDate = DateTime.UtcNow,
                UserCreateDate = new User(),
                LastChangeDate = DateTime.UtcNow,
                UserLastChangeDate = new User(),
                Deleted = true,
                
                PictureUrl = "PictureUrl",
                UserPromocodes = new List<UserPromocodes>(),
                UsersSubscriptionTotal = 2
            };

            discountResponse.Id.Should().NotBeEmpty();
            discountResponse.Name.Should().NotBeEmpty();
            discountResponse.Description.Should().NotBeEmpty();
            discountResponse.WorkingDaysOfTheWeek.Should().NotBeEmpty();

            discountResponse.StartDate.Should().NotBeNull();
            discountResponse.EndDate.Should().NotBeNull();
            discountResponse.CreateDate.Should().NotBeNull();
            discountResponse.LastChangeDate.Should().NotBeNull();
            discountResponse.Address.Should().NotBeNull();
            discountResponse.Company.Should().NotBeNull();
            discountResponse.Tags.Should().NotBeNull();
            discountResponse.UserCreateDate.Should().NotBeNull();
            discountResponse.UserLastChangeDate.Should().NotBeNull();

            discountResponse.Deleted.Should().BeTrue();

            discountResponse.AmountOfDiscount.Should().Be(1);
            discountResponse.RatingTotal.Should().Be(1);
            discountResponse.ViewsTotal.Should().Be(1);
            discountResponse.SubscriptionsTotal.Should().Be(1);
            discountResponse.Deleted.Should().BeTrue();

            discountResponse.PictureUrl.Should().NotBeNullOrEmpty();
            discountResponse.UserPromocodes.Should().NotBeNull();
            discountResponse.UsersSubscriptionTotal.Should().Be(2);
        }
    }
}
