using Exadel.CrazyPrice.Data.Models;
using MongoDB.Driver;

namespace Exadel.CrazyPrice.Data.Context
{
    public interface IDbContext
    {
        public IMongoCollection<DbAddress> Addresses { get; }

        public IMongoCollection<DbCompany> Companies { get; }

        public IMongoCollection<DbDiscount> Discounts { get; }

        public IMongoCollection<DbPerson> Persons { get; }

        public IMongoCollection<DbTag> Tags { get; }
    }
}
