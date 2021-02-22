using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace Exadel.CrazyPrice.Data.Models.Promocode
{
    public class DbPromocode
    {
        [BsonIgnoreIfNull]
        public string Id { get; set; }

        [BsonIgnoreIfNull]
        public DateTime? CreateDate { get; set; }

        [BsonIgnoreIfNull]
        public DateTime? EndDate { get; set; }

        [BsonIgnoreIfNull]
        public string PromocodeValue { get; set; }

        [BsonDefaultValue(false)]
        [JsonIgnore]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets a new DbPromocode with expiration date after limit days.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="promocodeId"></param>
        /// <param name="startDate"></param>
        /// <param name="promocodeValue"></param>
        /// <returns></returns>
        public static DbPromocode New(Guid promocodeId, DateTime startDate, int? limit, string promocodeValue) =>
            new()
            {
                Id = promocodeId.ToString(),
                CreateDate = startDate,
                EndDate = limit == null ? (DateTime?)null : startDate + TimeSpan.FromDays((double)limit),
                PromocodeValue = promocodeValue,
                Deleted = false
            };
    }
}