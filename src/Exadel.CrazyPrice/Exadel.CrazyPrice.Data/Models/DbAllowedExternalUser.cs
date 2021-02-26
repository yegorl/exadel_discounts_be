using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.Option;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models
{
    public class DbAllowedExternalUser
    {
        [BsonId]
        public string Id { get; set; }

        [BsonIgnoreIfNull]
        public string Mail { get; set; }

        [BsonIgnoreIfNull]
        [BsonIgnoreIfDefault]
        public RoleOption Roles { get; set; }
    }
}
