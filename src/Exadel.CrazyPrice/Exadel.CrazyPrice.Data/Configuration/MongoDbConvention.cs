using MongoDB.Bson.Serialization.Conventions;

namespace Exadel.CrazyPrice.Data.Configuration
{
    public static class MongoDbConvention
    {
        public static void SetCamelCaseElementNameConvention()
        {
            ConventionRegistry.Register("camelCase", new ConventionPack
            {
                new CamelCaseElementNameConvention()
            }, t => true);
        }
    }
}
