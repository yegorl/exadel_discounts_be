using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models
{
    /// <summary>
    /// Represents the DbTranslationCompany.
    /// </summary>
    public class DbTranslationCompany
    {
        [BsonIgnoreIfDefault]
        public string Name { get; set; }

        [BsonIgnoreIfDefault]
        public string Description { get; set; }
    }
}