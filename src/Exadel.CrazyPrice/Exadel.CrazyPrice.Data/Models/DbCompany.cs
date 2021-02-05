using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models
{
    public class DbCompany
    {
        [BsonIgnoreIfDefault]
        public string Name { get; set; }

        [BsonIgnoreIfDefault]
        public string Description { get; set; }

        [BsonIgnoreIfDefault]
        public string PhoneNumber { get; set; }

        [BsonIgnoreIfDefault]
        public string Mail { get; set; }
    }
}