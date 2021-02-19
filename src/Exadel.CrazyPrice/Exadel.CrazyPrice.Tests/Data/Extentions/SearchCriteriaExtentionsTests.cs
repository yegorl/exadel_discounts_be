using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Data.Extentions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Data.Extentions
{
    public class SearchCriteriaExtentionsTests
    {
        [Fact]
        public void GetSortTest()
        {
            var sort = new SearchCriteria
            {
                SearchSortFieldOption = SortFieldOption.DateStart,
                SearchSortOption = SortOption.Desc
            };

            sort.GetSort().Should().BeEquivalentTo("{ \"startDate\": -1, \"endDate\": 1}");
        }

        [Fact]
        public void GetSkipTest()
        {
            var sort = new SearchCriteria
            {
                SearchPaginationPageNumber = 2,
                SearchPaginationCountElementPerPage = 5
            };

            sort.GetSkip().Should().Be(5);
        }

        [Fact]
        public void GetQueryFreeTest()
        {
            var sort = new SearchCriteria
            {
                SearchPaginationPageNumber = 2,
                SearchPaginationCountElementPerPage = 5,
                SearchSortOption = SortOption.Asc,
                SearchLanguage = LanguageOption.Ru,
                SearchAddressCity = "",
                SearchAddressCountry = "",
                SearchDiscountOption = DiscountOption.All,
                SearchShowDeleted = false,
                SearchSortFieldOption = SortFieldOption.DateStart,
                SearchText = "",
                SearchUserId = Guid.Parse("82cabda2-2e10-4fe5-a78f-ade3bcb6d854"),
                SearchAdvanced = new SearchAdvancedCriteria()
                {
                    CompanyName = "AdvancedName",
                    SearchDate = new SearchDateCriteria()
                    {
                        SearchEndDate = "01.01.2021 10:10:10".GetDateTimeInvariant(),
                        SearchStartDate = "01.01.2021 10:10:10".GetDateTimeInvariant()
                    },
                    SearchAmountOfDiscount = new SearchAmountOfDiscountCriteria()
                    {
                        SearchAmountOfDiscountMin = 10,
                        SearchAmountOfDiscountMax = 100
                    },
                    SearchRatingTotal = new SearchRatingTotalCriteria()
                    {
                        SearchRatingTotalMin = 1,
                        SearchRatingTotalMax = 5
                    }
                }
            };

            sort.GetQuery().Should().BeEquivalentTo("{$and : [{\"address.country\" : \"\"}, {\"address.city\" : \"\"}, {\"language\" : \"russian\"}, {\"deleted\" : false}, {\"startDate\" : {$gte : ISODate(\"2021-01-01T07:10:10Z\")} }, {\"endDate\" : {$lte : ISODate(\"2021-01-01T07:10:10Z\")} }, {\"amountOfDiscount\" : {$gte : \"10\"} }, {\"amountOfDiscount\" : {$lte : \"100\"} }, {\"ratingTotal\" : {$gte : \"1\"} }, {\"ratingTotal\" : {$lte : \"5\"} }, {\"company.name\" : \"AdvancedName\"}]}");
        }

        [Fact]
        public void GetQueryTest()
        {
            var sort = new SearchCriteria
            {
                SearchPaginationPageNumber = 2,
                SearchPaginationCountElementPerPage = 5,
                SearchSortOption = SortOption.Asc,
                SearchLanguage = LanguageOption.Ru,
                SearchAddressCity = "City",
                SearchAddressCountry = "Country",
                SearchDiscountOption = DiscountOption.All,
                SearchShowDeleted = false,
                SearchSortFieldOption = SortFieldOption.DateStart,
                SearchText = "Text",
                SearchUserId = Guid.Parse("82cabda2-2e10-4fe5-a78f-ade3bcb6d854")
            };

            sort.GetQuery().Should().BeEquivalentTo("{$or : [{\"name\" : /.*Text.*/i},{\"tags\" : /.*Text.*/i},{\"description\" : /.*Text.*/i}], $and : [{\"address.country\" : \"Country\"}, {\"address.city\" : \"City\"}, {\"language\" : \"russian\"}, {\"deleted\" : false}]}");
        }

        [Fact]
        public void GetQueryFavoritesTest()
        {
            var sort = new SearchCriteria
            {
                SearchPaginationPageNumber = 2,
                SearchPaginationCountElementPerPage = 5,
                SearchSortOption = SortOption.Asc,
                SearchLanguage = LanguageOption.Ru,
                SearchAddressCity = "City",
                SearchAddressCountry = "Country",
                SearchDiscountOption = DiscountOption.Favorites,
                SearchShowDeleted = false,
                SearchSortFieldOption = SortFieldOption.DateStart,
                SearchText = "Text",
                SearchUserId = Guid.Parse("82cabda2-2e10-4fe5-a78f-ade3bcb6d854")
            };

            sort.GetQuery().Should().BeEquivalentTo("{$or : [{\"name\" : /.*Text.*/i},{\"tags\" : /.*Text.*/i},{\"description\" : /.*Text.*/i}], $and : [{\"address.country\" : \"Country\"}, {\"address.city\" : \"City\"}, {\"language\" : \"russian\"}, {\"deleted\" : false}, {\"favoritesUsersId\" : \"82cabda2-2e10-4fe5-a78f-ade3bcb6d854\"}]}");
        }

        [Fact]
        public void GetQuerySubscriptionsTest()
        {
            var sort = new SearchCriteria
            {
                SearchPaginationPageNumber = 2,
                SearchPaginationCountElementPerPage = 5,
                SearchSortOption = SortOption.Asc,
                SearchLanguage = LanguageOption.Ru,
                SearchAddressCity = "City",
                SearchAddressCountry = "Country",
                SearchDiscountOption = DiscountOption.Subscriptions,
                SearchShowDeleted = false,
                SearchSortFieldOption = SortFieldOption.DateStart,
                SearchText = "Text or tag",
                SearchUserId = Guid.Parse("82cabda2-2e10-4fe5-a78f-ade3bcb6d854")
            };

            sort.GetQuery().Should().BeEquivalentTo("{$or : [{\"name\" : /.*Text.*/i},{\"name\" : /.*tag.*/i},{\"tags\" : /.*Text.*/i},{\"tags\" : /.*tag.*/i},{\"description\" : /.*Text.*/i},{\"description\" : /.*tag.*/i}], $and : [{\"address.country\" : \"Country\"}, {\"address.city\" : \"City\"}, {\"language\" : \"russian\"}, {\"deleted\" : false}, {\"subscriptionsUsersId\" : \"82cabda2-2e10-4fe5-a78f-ade3bcb6d854\"}]}");
        }

        [Fact]
        public void CompileQueryTest()
        {
            var values = new Dictionary<string, string>()
            {
                {"manualLanguageControl", "false"},
                {"query", "query"},
                {"searchValue", "searchValue"}
            };
            values.CompileQuery().Should().NotBeEmpty();
        }
    }
}
