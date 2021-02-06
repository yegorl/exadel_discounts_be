using Exadel.CrazyPrice.Common.Configurations;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
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
            var users = await _users.FindSync(query,
                new FindOptions<DbUser> { Limit = 1 }).ToListAsync();

            if (users == null || users.Count == 0)
            {
                return new User();
            }

            var isGuid = Guid.TryParse(users[0].Id, out var guid);

            if (!isGuid)
            {
                return new User();
            }

            return new User
            {
                Id = guid,
                Name = users[0].Name,
                Surname = users[0].Surname,
                PhoneNumber = users[0].PhoneNumber,
                Mail = users[0].Mail,
                HashPassword = users[0].HashPassword,
                Salt = users[0].Salt,
                Roles = users[0].Roles
            };
        }
    }
}
