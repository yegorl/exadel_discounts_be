using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using MongoDB.Bson;

namespace Exadel.CrazyPrice.Data.Extentions
{
    public static class DiscountsStatisticCriteriaExtensions
    {
        public static string GetMatch(this DiscountsStatisticCriteria criteria)
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

        public static string GetGroup(this DiscountsStatisticCriteria criteria)
        {
            return "{_id: \"\", discountTotal: {$sum: 1}, viewsTotal: {$sum: \"$viewsTotal\"}, subscriptionsTotal: {$sum: \"$subscriptionsTotal\"}" +
                   ",inFavoritesList: {$sum: {$size: \"$favoritesUsersId\"}}, inSubscriptionsList: {$sum: {$size: \"$subscriptionsUsersId\"}}}";
        }

        private static string ToIsoDate(this DateTime? dateTime) =>
            dateTime == null ? string.Empty : $"ISODate(\"{new BsonDateTime((DateTime)dateTime)}\")";
    }
}
