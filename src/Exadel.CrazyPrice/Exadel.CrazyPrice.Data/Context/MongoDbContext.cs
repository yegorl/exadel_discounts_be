using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Exadel.CrazyPrice.Data.Context
{
    public class MongoDbContext : IDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext(IOptions<MongoDbConfiguration> configuration)
        {
            var client = new MongoClient(configuration.Value.ConnectionString);
            _db = client.GetDatabase(configuration.Value.Database);
        }

        public IMongoCollection<DbAddress> Addresses => _db.GetCollection<DbAddress>("Addresses");

        public IMongoCollection<DbCompany> Companies => _db.GetCollection<DbCompany>("Companies");

        public IMongoCollection<DbDiscount> Discounts => _db.GetCollection<DbDiscount>("Discounts");

        public IMongoCollection<DbPerson> Persons => _db.GetCollection<DbPerson>("Persons");

        public IMongoCollection<DbTag> Tags => _db.GetCollection<DbTag>("Tags");
    }
}