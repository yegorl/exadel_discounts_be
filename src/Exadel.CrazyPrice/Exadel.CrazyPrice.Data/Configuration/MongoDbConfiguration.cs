using Exadel.CrazyPrice.Common.Configurations;
using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.Extensions.Configuration;

namespace Exadel.CrazyPrice.Data.Configuration
{
    /// <summary>
    /// MongoDB configuration.
    /// </summary>
    public class MongoDbConfiguration : IDbConfiguration
    {
        public MongoDbConfiguration(IConfiguration configuration)
        {
            ConnectionString = configuration.GetString("Database:ConnectionStrings:DefaultConnection");
            Database = configuration.GetString("Database:ConnectionStrings:Database");
        }

        public string ConnectionString { get; }

        public string Database { get; }
    }
}
