using Exadel.CrazyPrice.Common.Extentions;
using FluentAssertions;
using System;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models.Promocode
{
    public class PromocodeTests
    {
        [Fact]
        public void PromocodeTest()
        {
            var promocode = new CrazyPrice.Common.Models.Promocode.Promocode
            {
                Id = Guid.Parse("dc666524-36a3-43ef-998c-7f250793d9bc"),
                CreateDate = "01.01.2020 10:10:10".GetUtcDateTime(),
                EndDate = "01.01.2021 10:10:10".GetUtcDateTime(),
                Deleted = false,
                PromocodeValue = StringExtentions.NewPromocodeValue()
            };

            promocode.CreateDate.Should().NotBeNull();
            promocode.EndDate.Should().NotBeNull();
            promocode.PromocodeValue.Should().NotBeNull();
            promocode.Id.Should().NotBeEmpty();
            promocode.Expired.Should().BeTrue();
            promocode.Deleted.Should().BeFalse();

        }
    }
}
