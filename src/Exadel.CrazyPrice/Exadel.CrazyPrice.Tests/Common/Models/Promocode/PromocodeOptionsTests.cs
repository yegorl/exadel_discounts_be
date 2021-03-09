using Exadel.CrazyPrice.Common.Models.Promocode;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models.Promocode
{
    public class PromocodeOptionsTests
    {
        [Theory]
        [InlineData(122, 2, 4, 5)]
        [InlineData(0, 1, 0, 9)]
        [InlineData(null, null, 4, 5)]
        [InlineData(10, null, 4, 5)]
        [InlineData(122, null, 4, null)]
        public void PromocodeOptionsTest(
            int? countActivePromocodePerUser,
            int? countSymbolsPromocode,
            int? daysDurationPromocode,
            int? timeLimitAddingInSeconds
        )
        {
            var promocodeOptions = new PromocodeOptions()
            {
                CountActivePromocodePerUser = countActivePromocodePerUser,
                CountSymbolsPromocode = countSymbolsPromocode,
                DaysDurationPromocode = daysDurationPromocode,
                TimeLimitAddingInSeconds = timeLimitAddingInSeconds,
                EnabledPromocodes = true
            };

            promocodeOptions.CountActivePromocodePerUser.Should().Be(countActivePromocodePerUser);
            promocodeOptions.CountSymbolsPromocode.Should().Be(countSymbolsPromocode);
            promocodeOptions.DaysDurationPromocode.Should().Be(daysDurationPromocode);
            promocodeOptions.TimeLimitAddingInSeconds.Should().Be(timeLimitAddingInSeconds);
            promocodeOptions.EnabledPromocodes.Should().BeTrue();
        }
    }
}
