using Exadel.CrazyPrice.Common.Models.Option;
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
        
        public string Mail { get; set; }

        public string HashPassword { get; set; }

        public string Salt { get; set; }

        public RoleOption Roles { get; set; }
    }
}
