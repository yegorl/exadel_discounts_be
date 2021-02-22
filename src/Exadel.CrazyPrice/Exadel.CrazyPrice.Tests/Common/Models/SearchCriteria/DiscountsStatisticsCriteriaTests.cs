using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models.SearchCriteria
{
    public class DiscountsStatisticsCriteriaTests
    {
        [Fact]
        public void DiscountsStatisticsCriteriaTest()
        {
            var discountsStatisticsCriteria = new DiscountsStatisticsCriteria()
            {
                CreateStartDate = "01.01.2020 10:10:10".GetUtcDateTime(),
                CreateEndDate = "01.01.2021 10:10:10".GetUtcDateTime(),
                SearchAddressCity = "SearchAddressCity",
                SearchAddressCountry = "SearchAddressCountry"
            };

            discountsStatisticsCriteria.SearchAddressCountry.Should().NotBeNull();
            discountsStatisticsCriteria.SearchAddressCity.Should().NotBeNull();
            discountsStatisticsCriteria.CreateEndDate.Should().NotBeNull();
            discountsStatisticsCriteria.CreateStartDate.Should().NotBeNull();
        }
    }
}
