using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Data.Models
{
    /// <summary>
    /// Represents the DbTranslation.
    /// </summary>
    public class DbTranslation
    {
        [BsonIgnoreIfNull]
        public string Name { get; set; }

        [BsonIgnoreIfNull]
        public string Description { get; set; }

        [BsonIgnoreIfNull]
        public DbTranslationAddress Address { get; set; }

        [BsonIgnoreIfNull]
        public DbTranslationCompany Company { get; set; }

        [BsonIgnoreIfNull]
        public List<string> Tags { get; set; }

        [BsonIgnoreIfNull]
        public string Language { get; set; }
    }
}