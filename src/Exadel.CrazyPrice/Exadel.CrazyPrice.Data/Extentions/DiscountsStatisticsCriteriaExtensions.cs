﻿using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using MongoDB.Bson;
using System;
using System.Text;

namespace Exadel.CrazyPrice.Data.Extentions
{
    public static class DiscountsStatisticsCriteriaExtensions
    {
        public static string GetMatch(this DiscountsStatisticsCriteria criteria)
        {
            var matchBuilder = new StringBuilder();
            matchBuilder.Append("{");
            if (criteria.SearchAddressCountry != null)
            {
                matchBuilder.Append($"\"address.country\": \"{criteria.SearchAddressCountry}\", ");
                if (criteria.SearchAddressCity != null)
                {
                    matchBuilder.Append($"\"address.city\": \"{criteria.SearchAddressCity}\" ");
                }
            }

            if (criteria.CreateStartDate != null || criteria.CreateEndDate != null)
            {
                matchBuilder.Append("\"createDate\": {");
                if (criteria.CreateStartDate != null)
                {
                    matchBuilder.Append($"$gt: {criteria.CreateStartDate.ToIsoDate()},");
                }
                if (criteria.CreateEndDate != null)
                {
                    matchBuilder.Append($"$lt: {criteria.CreateEndDate.ToIsoDate()}");
                }

                matchBuilder.Append("}");
            }

            matchBuilder.Append("}");
            return matchBuilder.ToString();
        }

        public static string GetGroup(this DiscountsStatisticsCriteria criteria)
        {
            return "{_id: \"\", discountTotal: {$sum: 1}, viewsTotal: {$sum: \"$viewsTotal\"}, subscriptionsTotal: {$sum: \"$subscriptionsTotal\"}" +
                   ",inFavoritesList: {$sum: {$size: \"$favoritesUsersId\"}}, inSubscriptionsList: {$sum: {$size: \"$subscriptionsUsersId\"}}}";
        }

        private static string ToIsoDate(this DateTime? dateTime) =>
            dateTime == null ? string.Empty : $"ISODate(\"{new BsonDateTime((DateTime)dateTime)}\")";
    }
}