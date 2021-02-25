using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models
{
    /// <summary>
    /// Represents the DbTranslationAddress.
    /// </summary>
    public class DbTranslationAddress
    {
        [BsonIgnoreIfNull]
        public string Country { get; set; }

        [BsonIgnoreIfNull]
        public string City { get; set; }

        [BsonIgnoreIfNull]
        public string Street { get; set; }
    }
}