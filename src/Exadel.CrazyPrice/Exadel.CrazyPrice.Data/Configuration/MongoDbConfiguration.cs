using Exadel.CrazyPrice.Common.Configurations;

namespace Exadel.CrazyPrice.Data.Configuration
{
    /// <summary>
    /// MongoDB configuration.
    /// </summary>
    public class MongoDbConfiguration : IDbConfiguration
    {
        public string ConnectionString { get; set; }

        public string Database { get; set; }
    }
}
