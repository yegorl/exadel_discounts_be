using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.CrazyPrice.Data.Models
{
    /// <summary>
    /// Represents the DbLocation.
    /// </summary>
    public class DbLocation
    {
        [BsonIgnoreIfDefault]
        public double Latitude { get; set; }

        [BsonIgnoreIfDefault]
        public double Longitude { get; set; }
    }
}
