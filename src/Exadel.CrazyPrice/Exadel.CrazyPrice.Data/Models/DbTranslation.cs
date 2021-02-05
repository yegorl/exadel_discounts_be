using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Data.Models
{
    public class DbTranslation
    {
        [BsonIgnoreIfNull]
        public string Language { get; set; }

        [BsonIgnoreIfNull]
        public string Name { get; set; }

        [BsonIgnoreIfNull]
        public string Description { get; set; }

        [BsonIgnoreIfNull]
        public DbAddress Address { get; set; }

        [BsonIgnoreIfNull]
        public DbCompany Company { get; set; }

        [BsonIgnoreIfNull]
        public List<string> Tags { get; set; }
    }
}