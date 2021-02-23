using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.Option;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models
{
    public class DbExternalUser
    {
        [BsonId]
        public string Id { get; set; }

        [BsonIgnoreIfNull]
        public string Mail { get; set; }
    }
}
