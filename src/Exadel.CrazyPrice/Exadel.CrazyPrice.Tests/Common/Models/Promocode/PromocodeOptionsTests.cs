using Exadel.CrazyPrice.Common.Models.Promocode;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models.Promocode
{
    public class PromocodeOptionsTests
    {
        [Fact]
        public void PromocodeOptionsTest()
        {
            var promocodeOptions = new PromocodeOptions()
            {
                CountActivePromocodePerUser = 5,
                CountSymbolsPromocode = 6,
                DaysDurationPromocode = 7,
                TimeLimitAddingInSeconds = 8
            };

            promocodeOptions.CountActivePromocodePerUser.Should().Be(5);
            promocodeOptions.CountSymbolsPromocode.Should().Be(6);
            promocodeOptions.DaysDurationPromocode.Should().Be(7);
            promocodeOptions.TimeLimitAddingInSeconds.Should().Be(8);
        }
    }
}
