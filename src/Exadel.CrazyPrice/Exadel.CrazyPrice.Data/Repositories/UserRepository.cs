using Exadel.CrazyPrice.Common.Configurations;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Data.Repositories
{
    /// <summary>
    /// Represents the user repository.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<DbUser> _users;
        private readonly IMongoCollection<DbExternalUser> _externalUsers;

        /// <summary>
        /// Creates the user repository.
        /// </summary>
        /// <param name="configuration"></param>
        public UserRepository(IDbConfiguration configuration)
        {
            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);
            _users = db.GetCollection<DbUser>("Users");
            _externalUsers = db.GetCollection<DbExternalUser>("ExternalUsers");
        }

        public async Task AddUserAsunc(User user)
        {
            await _users.InsertOneAsync(user.ToDbUser());
        }

        public async Task<ExternalUser> GetExternalUserByEmailAsunc(string mail)
        {
            var list = await (_externalUsers.FindSync(Builders<DbExternalUser>.Filter.Eq(e => e.Mail, mail)).ToListAsync());
            
             return list.GetOne().ToExretnalUser();
        }

        /// <summary>
        /// Gets user by email.
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmailAsync(string mail) =>
            await GetUserAsync(Builders<DbUser>.Filter.Eq(d => d.Mail, mail));

        /// <summary>
        /// Gets user by uid.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<User> GetUserByUidAsync(Guid uid) =>
            await GetUserAsync(Builders<DbUser>.Filter.Eq(d => d.Id, uid.ToString()));

        private async Task<User> GetUserAsync(FilterDefinition<DbUser> filter) =>
            (await _users.FindSync(filter, new FindOptions<DbUser> { Limit = 1 }).ToListAsync()).GetOne().ToUser();
    }
}
