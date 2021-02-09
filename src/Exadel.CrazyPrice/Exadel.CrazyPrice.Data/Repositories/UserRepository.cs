using Exadel.CrazyPrice.Common.Configurations;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<DbUser> _users;

        public UserRepository(IDbConfiguration configuration)
        {
            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);
            _users = db.GetCollection<DbUser>("Users");
        }

        public async Task<User> GetUserByEmailAsync(string mail)
        {
            return await GetUserAsync("{ \"mail\" : \"" + mail + "\" }");
        }

        public async Task<User> GetUserByUidAsync(Guid uid)
        {
            return await GetUserAsync("{ \"_id\" : \"" + uid + "\" }");
        }

        private async Task<User> GetUserAsync(string query)
        {
            return (await _users.FindSync(query, new FindOptions<DbUser> { Limit = 1 }).ToListAsync()).GetOne().ToUser();
        }
    }
}
