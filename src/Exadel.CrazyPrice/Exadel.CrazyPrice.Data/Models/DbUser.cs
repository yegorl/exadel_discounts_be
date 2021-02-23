using System.Collections.Generic;
using Exadel.CrazyPrice.Common.Models.Option;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models
{
    /// <summary>
    /// Represents the DbUser.
    /// </summary>
    public class DbUser
    {
        [BsonId]
        public string Id { get; set; }
        
        [BsonIgnoreIfNull]
        public string Name { get; set; }

        [BsonIgnoreIfNull]
        public string Surname { get; set; }

        [BsonIgnoreIfNull]
        public string PhoneNumber { get; set; }

        [BsonIgnoreIfNull]
        public string Mail { get; set; }

        [BsonIgnoreIfNull]
        public string HashPassword { get; set; }

        [BsonIgnoreIfNull]
        public string Salt { get; set; }

        [BsonIgnoreIfNull]
        [BsonIgnoreIfDefault]
        public RoleOption Roles { get; set; }

        [BsonIgnoreIfNull]
        [BsonIgnoreIfDefault]
        public UserTypeOption Type { get; set; }

        [BsonIgnoreIfNull]
        [BsonIgnoreIfDefault]
        public ProviderOptions Provider { get; set; }
    }
}
