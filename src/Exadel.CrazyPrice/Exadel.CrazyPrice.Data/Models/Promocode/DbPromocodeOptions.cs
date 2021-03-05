using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models.Promocode
{
    public class DbPromocodeOptions
    {
        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [BsonDefaultValue(false)]
        public bool? EnabledPromocodes { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [BsonDefaultValue(5)]
        public int? CountActivePromocodePerUser { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [BsonDefaultValue(7)]
        public int? DaysDurationPromocode { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [BsonDefaultValue(5)]
        public int? CountSymbolsPromocode { get; set; }

        [BsonIgnoreIfDefault]
        [BsonIgnoreIfNull]
        [BsonDefaultValue(5)]
        public int? TimeLimitAddingInSeconds { get; set; }
    }
}
