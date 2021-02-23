using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using FluentAssertions;
using System;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Models.SearchCriteria
{
    public class SearchCriteriaTests
    {
        [Fact]
        public void SearchCriteriaTest()
        {
            var searchCriteria = new CrazyPrice.Common.Models.SearchCriteria.SearchCriteria()
            {
                SearchLanguage = LanguageOption.En,
                SearchAddressCity = "SearchAddressCity",
                SearchAddressCountry = "SearchAddressCountry",
                SearchDiscountOption = DiscountOption.All,
                SearchPaginationCountElementPerPage = 5,
                SearchPaginationPageNumber = 1,
                SearchShowDeleted = false,
                SearchSortFieldOption = SortFieldOption.DateStart,
                SearchSortOption = SortOption.Asc,
                SearchText = "SearchText",
                IncomingUser = new IncomingUser()
                {
                    Id = Guid.Parse("f22960bc-b378-4d2f-b546-eb9700156f8f"),
                    Role = RoleOption.Employee
                },
                SearchAdvanced = new SearchAdvancedCriteria()
                {
                    CompanyName = "CompanyName",
                    SearchDate = new SearchDateCriteria()
                    {
                        SearchStartDate = DateTime.UtcNow,
                        SearchEndDate = DateTime.UtcNow
                    },
                    SearchAmountOfDiscount = new SearchAmountOfDiscountCriteria()
                    {
                        SearchAmountOfDiscountMax = 1,
                        SearchAmountOfDiscountMin = 0
                    },
                    SearchRatingTotal = new SearchRatingTotalCriteria()
                    {
                        SearchRatingTotalMin = 0,
                        SearchRatingTotalMax = 5
                    }
                }
            };

            searchCriteria.SearchLanguage.Should().NotBeNull();
            searchCriteria.SearchAddressCity.Should().NotBeNull();
            searchCriteria.SearchAddressCountry.Should().NotBeNull();
            searchCriteria.SearchDiscountOption.Should().NotBeNull();
            searchCriteria.SearchPaginationCountElementPerPage.Should().BeGreaterThan(0);
            searchCriteria.SearchPaginationPageNumber.Should().BeGreaterThan(0);
            searchCriteria.SearchShowDeleted.Should().BeFalse();
            searchCriteria.SearchSortFieldOption.Should().NotBeNull();
            searchCriteria.SearchSortOption.Should().NotBeNull();
            searchCriteria.SearchText.Should().NotBeNull();
            searchCriteria.IncomingUser.Id.Should().NotBeEmpty();
            searchCriteria.SearchAdvanced.Should().NotBeNull();

            searchCriteria.SearchAdvanced.CompanyName.Should().NotBeNull();

            searchCriteria.SearchAdvanced.SearchDate.Should().NotBeNull();
            searchCriteria.SearchAdvanced.SearchDate.SearchStartDate.Should().NotBeNull();
            searchCriteria.SearchAdvanced.SearchDate.SearchEndDate.Should().NotBeNull();

            searchCriteria.SearchAdvanced.SearchAmountOfDiscount.Should().NotBeNull();
            searchCriteria.SearchAdvanced.SearchAmountOfDiscount.SearchAmountOfDiscountMin.Should().NotBeNull();
            searchCriteria.SearchAdvanced.SearchAmountOfDiscount.SearchAmountOfDiscountMax.Should().NotBeNull();

            searchCriteria.SearchAdvanced.SearchRatingTotal.Should().NotBeNull();
            searchCriteria.SearchAdvanced.SearchRatingTotal.SearchRatingTotalMin.Should().NotBeNull();
            searchCriteria.SearchAdvanced.SearchRatingTotal.SearchRatingTotalMax.Should().NotBeNull();
        }
    }
}
