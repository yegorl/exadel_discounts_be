using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Exadel.CrazyPrice.Data.Models
{
    /// <summary>
    /// Represents the DbDiscount.
    /// </summary>
    public class DbDiscount
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }

        [BsonIgnoreIfNull]
        public string Description { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public decimal? AmountOfDiscount { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public DateTime? StartDate { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        public DateTime? EndDate { get; set; }

        [BsonIgnoreIfNull]
        public DbAddress Address { get; set; }

        [BsonIgnoreIfNull]
        public DbCompany Company { get; set; }

        [BsonIgnoreIfNull]
        public string WorkingDaysOfTheWeek { get; set; }

        [BsonIgnoreIfNull]
        public List<string> Tags { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public decimal? RatingTotal { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public int? ViewsTotal { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public int? SubscriptionsTotal { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public List<string> FavoritesUsersId { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public List<string> SubscriptionsUsersId { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public List<string> RatingUsersId { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public DateTime? CreateDate { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public DbUser UserCreateDate { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [JsonIgnore]
        public DateTime? LastChangeDate { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public DbUser UserLastChangeDate { get; set; }

        [BsonDefaultValue(false)]
        [JsonIgnore]
        public bool Deleted { get; set; }
        
        [BsonIgnoreIfNull]
        public string Language { get; set; }

        [BsonIgnoreIfNull]
        public List<DbTranslation> Translations { get; set; }
    }
}
