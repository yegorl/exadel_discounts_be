using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Exadel.CrazyPrice.Data.Models
{
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
        public DateTime StartDate { get; set; }

        [BsonIgnoreIfDefault]
        public DateTime EndDate { get; set; }

        [BsonIgnoreIfNull]
        public DbAddress Address { get; set; }

        [BsonIgnoreIfNull]
        public DbCompany Company { get; set; }

        [BsonIgnoreIfNull]
        public string WorkingHours { get; set; }

        [BsonIgnoreIfNull]
        public List<string> Tags { get; set; }

        [BsonIgnoreIfDefault]
        [JsonIgnore]
        public float RatingTotal { get; set; }

        [BsonIgnoreIfDefault]
        [JsonIgnore]
        public int ViewTotal { get; set; }

        [BsonIgnoreIfDefault]
        [JsonIgnore]
        public int ReservationTotal { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public List<string> FavoritePersonsId { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public List<string> ReservationPersonsId { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public List<string> ViewPersonsId { get; set; }

        [BsonIgnoreIfDefault]
        [JsonIgnore]
        public DateTime CreateDate { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public DbUser PersonCreateDate { get; set; }

        [BsonIgnoreIfDefault]
        [JsonIgnore]
        public DateTime LastChangeDate { get; set; }

        [BsonIgnoreIfNull]
        [JsonIgnore]
        public DbUser PersonLastChangeDate { get; set; }

        [BsonDefaultValue(false)]
        [JsonIgnore]
        public bool Hidden { get; set; }
        
        [BsonIgnoreIfNull]
        public string Language { get; set; }

        [BsonIgnoreIfNull]
        public List<DbTranslation> Translations { get; set; }
    }
}
