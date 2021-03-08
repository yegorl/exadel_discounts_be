using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exadel.CrazyPrice.Data.Extentions
{
    /// <summary>
    /// Represent methods for search.
    /// </summary>
    public static class SearchCriteriaExtentions
    {
        /// <summary>
        /// Gets sort.
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public static string GetSort(this SearchCriteria searchCriteria)
        {
            var startDateSort = searchCriteria.SearchSortFieldOption == SortFieldOption.DateStart ? "" : ", \"startDate\": -1";
            var endDateSort = searchCriteria.SearchSortFieldOption == SortFieldOption.DateEnd ? "" : ", \"endDate\": 1";

            return "{ \"" + searchCriteria.GetTranslationsPrefixForSortField() + searchCriteria.SearchSortFieldOption.ToStringLookup() + "\": " +
                   (int)searchCriteria.SearchSortOption + startDateSort + endDateSort + "}";
        }

        /// <summary>
        /// Gets skip.
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public static int GetSkip(this SearchCriteria searchCriteria) =>
            (searchCriteria.SearchPaginationPageNumber - 1) * searchCriteria.SearchPaginationCountElementPerPage;

        /// <summary>
        /// Gets query.
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns></returns>
        public static string GetQuery(this SearchCriteria searchCriteria)
        {
            searchCriteria.SearchText = searchCriteria.SearchText.GetValidContent(CharOptions.Letter, " ");

            searchCriteria.SearchShowDeleted = searchCriteria.CurrentUser.Role == RoleOption.Administrator;

            var queryBuilder = new StringBuilder();
            queryBuilder.Append('{');

            var containsСondition = searchCriteria.GetContainsСondition();
            var isOrContainsСondition = containsСondition.Substring(0, 3).ToLowerInvariant() == "$or";

            if (!searchCriteria.SearchText.IsNullOrEmpty() && isOrContainsСondition)
            {
                queryBuilder.Append(searchCriteria.GetContainsСondition() + ", ");
            }

            queryBuilder.Append("$and : [" +
                                searchCriteria.GetEqualsСondition() +

                                searchCriteria.GetDeletedСondition() +
                                searchCriteria.GetDiscountOptionCondition() +
                                searchCriteria.GetDateCondition() +
                                searchCriteria.GetAmountOfDiscountCondition() +
                                searchCriteria.GetRatingTotalCondition() +
                                searchCriteria.GetCompanyNameCondition());
            if (!isOrContainsСondition)
            {
                queryBuilder.Append(", " + searchCriteria.GetContainsСondition());
            }

            queryBuilder.Append(']');
            queryBuilder.Append('}');

            var queryCompile = new Dictionary<string, string>
            {
                { "manualLanguageControl", "true" },

                { "searchCountry", searchCriteria.SearchAddressCountry },
                { "searchCity", searchCriteria.SearchAddressCity },
                { "searchLanguage", searchCriteria.SearchLanguage.ToStringLookup() },

                { "fieldName", "name" },
                { "fieldTags", "tags" },
                { "fieldDescription", "description" },
                { "fieldCountry", "address.country" },
                { "fieldCity", "address.city" },
                { "fieldLanguage", "language" },
                { "fieldTranslations", searchCriteria.GetTranslationsPrefix() },

                { "query", queryBuilder.ToString()}
            }.CompileQuery();

            return queryCompile["query"];
        }

        /// <summary>
        /// Gets query params.
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public static Dictionary<string, string> CompileQuery(this Dictionary<string, string> queryParams)
        {
            string translations;

            if (!queryParams.Keys.Contains("manualLanguageControl") || queryParams["manualLanguageControl"].ToLowerInvariant() == "false")
            {
                translations = LanguageOption.En == queryParams["searchValue"].GetLanguageFromFirstLetter()
                    ? "translations." : string.Empty;
            }
            else
            {
                translations = string.Empty;
            }

            foreach (var (key, value) in queryParams)
            {
                if (key.StartsWith("search", StringComparison.OrdinalIgnoreCase))
                {
                    queryParams["query"] = queryParams["query"].Replace($"%{key}%", value);
                    continue;
                }

                if (key.StartsWith("field", StringComparison.OrdinalIgnoreCase))
                {
                    queryParams[key] = $"{translations}{value}";
                    queryParams["query"] = queryParams["query"].Replace($"%{key}%", queryParams[key]);
                }
            }

            return queryParams;
        }

        public static bool IsNotAdministratorSortByDateCreate(this SearchCriteria searchCriteria, RoleOption role) =>
            searchCriteria.SearchSortFieldOption == SortFieldOption.DateCreate && role != RoleOption.Administrator;

        private static string GetDateCondition(this SearchCriteria searchCriteria)
        {
            var builder = new StringBuilder();
            var date = searchCriteria.GetStartDate();
            if (!string.IsNullOrEmpty(date))
            {
                builder.Append(", {\"startDate\" : {$lte : " + date + "} }");
            }

            date = searchCriteria.GetEndDate();
            if (!string.IsNullOrEmpty(date))
            {
                builder.Append(", {\"endDate\" : {$gte : " + date + "} }");
            }

            return builder.ToString();
        }

        private static string GetStartDate(this SearchCriteria searchCriteria) =>
            searchCriteria.SearchAdvanced?.SearchDate?.SearchStartDate == null
                ? string.Empty
                : searchCriteria.SearchAdvanced.SearchDate.SearchStartDate.ToIsoDate();

        private static string GetEndDate(this SearchCriteria searchCriteria) =>
            searchCriteria.SearchAdvanced?.SearchDate?.SearchEndDate == null
                ? string.Empty
                : searchCriteria.SearchAdvanced.SearchDate.SearchEndDate.ToIsoDate();

        private static string GetAmountOfDiscountCondition(this SearchCriteria searchCriteria)
        {
            var builder = new StringBuilder();
            var value = searchCriteria.GetAmountOfDiscountMin();
            if (!string.IsNullOrEmpty(value))
            {
                builder.Append(", {\"amountOfDiscount\" : {$gte : " + value + "} }");
            }

            value = searchCriteria.GetAmountOfDiscountMax();
            if (!string.IsNullOrEmpty(value))
            {
                builder.Append(", {\"amountOfDiscount\" : {$lte : " + value + "} }");
            }

            return builder.ToString();
        }

        private static string GetAmountOfDiscountMin(this SearchCriteria searchCriteria) =>
            searchCriteria.SearchAdvanced?.SearchAmountOfDiscount?.SearchAmountOfDiscountMin == null
                ? string.Empty
                : searchCriteria.SearchAdvanced.SearchAmountOfDiscount.SearchAmountOfDiscountMin == 0
                    ? string.Empty
                    : searchCriteria.SearchAdvanced.SearchAmountOfDiscount.SearchAmountOfDiscountMin.ToString();

        private static string GetAmountOfDiscountMax(this SearchCriteria searchCriteria) =>
            searchCriteria.SearchAdvanced?.SearchAmountOfDiscount?.SearchAmountOfDiscountMax == null
                ? string.Empty
                : searchCriteria.SearchAdvanced.SearchAmountOfDiscount.SearchAmountOfDiscountMax == 0
                    ? string.Empty
                    : searchCriteria.SearchAdvanced.SearchAmountOfDiscount.SearchAmountOfDiscountMax.ToString();

        private static string GetRatingTotalCondition(this SearchCriteria searchCriteria)
        {
            var builder = new StringBuilder();
            var value = searchCriteria.GetRatingTotalMin();
            if (!string.IsNullOrEmpty(value))
            {
                builder.Append(", {\"ratingTotal\" : {$gte : " + value + "} }");
            }

            value = searchCriteria.GetRatingTotalMax();
            if (!string.IsNullOrEmpty(value))
            {
                builder.Append(", {\"ratingTotal\" : {$lte : " + value + "} }");
            }

            return builder.ToString();
        }

        private static string GetRatingTotalMin(this SearchCriteria searchCriteria) =>
            searchCriteria.SearchAdvanced?.SearchRatingTotal?.SearchRatingTotalMin == null
                ? string.Empty
                : searchCriteria.SearchAdvanced.SearchRatingTotal.SearchRatingTotalMin == 0
                    ? string.Empty
                    : searchCriteria.SearchAdvanced.SearchRatingTotal.SearchRatingTotalMin.ToString();

        private static string GetRatingTotalMax(this SearchCriteria searchCriteria) =>
            searchCriteria.SearchAdvanced?.SearchRatingTotal?.SearchRatingTotalMax == null
                ? string.Empty
                : searchCriteria.SearchAdvanced.SearchRatingTotal.SearchRatingTotalMax == 0
                    ? string.Empty
                    : searchCriteria.SearchAdvanced.SearchRatingTotal.SearchRatingTotalMax.ToString();

        private static string GetCompanyName(this SearchCriteria searchCriteria) =>
            searchCriteria.SearchAdvanced?.CompanyName ?? string.Empty;

        private static string ToIsoDate(this DateTime? dateTime) =>
            dateTime == null ? string.Empty : $"ISODate(\"{new BsonDateTime((DateTime)dateTime)}\")";

        private static string GetCompanyNameCondition(this SearchCriteria searchCriteria)
        {
            var value = searchCriteria.GetCompanyName();
            return string.IsNullOrEmpty(value)
                ? string.Empty
                : ", {\"%fieldTranslations%company.name\" : \"" + value + "\"}";
        }

        private static string GetDiscountOptionCondition(this SearchCriteria searchCriteria) =>
            searchCriteria.SearchDiscountOption switch
            {
                DiscountOption.Favorites => ", {\"favoritesUsersId\" : \"" + searchCriteria.CurrentUser.Id + "\"}",
                DiscountOption.Subscriptions => ", {\"usersPromocodes.userId\" : \"" + searchCriteria.CurrentUser.Id + "\", \"usersPromocodes.promocodes.deleted\" : false }",
                _ => string.Empty
            };

        private static string GetDeletedСondition(this SearchCriteria searchCriteria)
            => searchCriteria.SearchShowDeleted ? string.Empty : ", {\"deleted\" : false}";

        private static string GetEqualsСondition(this SearchCriteria searchCriteria)
        {
            var stringBuilder = new StringBuilder();
            if (searchCriteria.SearchDiscountOption == DiscountOption.All)
            {
                stringBuilder.Append(searchCriteria.GetEqualsСondition("Country") + ", ");
                stringBuilder.Append(searchCriteria.GetEqualsСondition("City") + ", ");
                stringBuilder.Append(searchCriteria.GetEqualsСondition("Language"));
            }
            else
            {
                stringBuilder.Append("{\"_id\" : {$ne : null } }");
            }
            return stringBuilder.ToString();
        }

        private static string GetEqualsСondition(this SearchCriteria searchCriteria, string field, string overrideSearchPostfix = "")
            => "{\"%fieldTranslations%%field" + field + "%\" : \"%search" + (overrideSearchPostfix == "" ? field : overrideSearchPostfix) + "%\"}";

        private static string GetContainsСondition(string field, string value = "")
            => "{\"%fieldTranslations%%field" + field + "%\" : /.*" + value + ".*/i}";

        private static string GetContainsСondition(this SearchCriteria searchCriteria)
        {
            var stringBuilder = new StringBuilder();

            if (searchCriteria.SearchAdvanced == null)
            {
                return $"$or : [{searchCriteria.GetContainsСondition(new[] {"Name", "Tags", "Description"})}]";
            }

            switch (searchCriteria.SearchAdvanced.SearchOnlyTagsOption)
            {
                case SearchOnlyTagsOption.And:
                    stringBuilder.Append(searchCriteria.GetContainsСondition(new[] { "Tags" }));
                    break;
                case SearchOnlyTagsOption.Or:
                    stringBuilder.Append($"$or : [{searchCriteria.GetContainsСondition(new[] { "Tags" })}]");
                    break;
                default:
                    stringBuilder.Append($"$or : [{searchCriteria.GetContainsСondition(new[] { "Name", "Tags", "Description" })}]");
                    break;
            }

            return stringBuilder.ToString();
        }

        private static string GetContainsСondition(this SearchCriteria searchCriteria, string[] fields)
        {
            var words = searchCriteria.SearchText.Split(" ");
            var builder = new StringBuilder();
            var comma = ',';
            foreach (var field in fields)
            {
                foreach (var word in words)
                {
                    if (word.Length < 3)
                    {
                        continue;
                    }

                    builder.Append(GetContainsСondition(field, word) + comma);
                }
            }

            return builder.ToString().Trim(comma);
        }
        
        private static string GetTranslationsPrefix(this SearchCriteria searchCriteria) =>
            "".GetWithTranslationsPrefix(searchCriteria.SearchLanguage, true);

        private static string GetTranslationsPrefixForSortField(this SearchCriteria searchCriteria) =>
            searchCriteria.SearchSortFieldOption is SortFieldOption.NameDiscount or SortFieldOption.CompanyName
                ? "".GetWithTranslationsPrefix(searchCriteria.SearchLanguage, true)
                : string.Empty;
    }
}
