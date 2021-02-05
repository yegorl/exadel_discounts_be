using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models
{
    public class DbAddress
    {
        [BsonIgnoreIfNull]
        public string Country { get; set; }

        [BsonIgnoreIfNull]
        public string City { get; set; }

        [BsonIgnoreIfNull]
        public string Street { get; set; }

        [BsonIgnoreIfNull]
        public DbLocation Location { get; set; }
    }
}