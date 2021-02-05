using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Data.Configuration;
using Exadel.CrazyPrice.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<DbUser> _users;

        public UserRepository(IOptions<MongoDbConfiguration> configuration)
        {
            var client = new MongoClient(configuration.Value.ConnectionString);
            var db = client.GetDatabase(configuration.Value.Database);
            _users = db.GetCollection<DbUser>("Users");
        }

        public Task<User> GetUserByEmailAsync(string mail)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUidAsync(Guid uid)
        {
            throw new NotImplementedException();
        }
    }
}
