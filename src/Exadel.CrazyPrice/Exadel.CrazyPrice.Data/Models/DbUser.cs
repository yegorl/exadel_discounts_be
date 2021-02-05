using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models
{
    public class DbUser
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }

        [BsonIgnoreIfNull]
        public string Surname { get; set; }

        [BsonIgnoreIfNull]
        public string PhoneNumber { get; set; }

        [BsonIgnoreIfNull]
        public string Mail { get; set; }
    }
}
