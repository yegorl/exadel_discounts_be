using Exadel.CrazyPrice.Common.Configurations;
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
            ConnectionString = configuration.GetSection("Database:ConnectionStrings:DefaultConnection").Value;
            Database = configuration.GetSection("Database:ConnectionStrings:Database").Value;
        }

        public string ConnectionString { get; }

        public string Database { get; }
    }
}
