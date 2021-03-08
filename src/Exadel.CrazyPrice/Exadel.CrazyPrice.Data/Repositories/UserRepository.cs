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
using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Option;
using MongoDB.Bson;

namespace Exadel.CrazyPrice.Data.Repositories
{
    /// <summary>
    /// Represents the user repository.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<DbUser> _users;
        private readonly IMongoCollection<DbAllowedExternalUser> _allowedExternalUser;

        /// <summary>
        /// Creates the user repository.
        /// </summary>
        /// <param name="configuration"></param>
        public UserRepository(IDbConfiguration configuration)
        {
            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);
            _users = db.GetCollection<DbUser>("Users");
            _allowedExternalUser = db.GetCollection<DbAllowedExternalUser>("AllowedExternalUsers");
        }

        /// <summary>
        /// Adds externalUser into user async
        /// </summary>
        /// <param name="externalUser"></param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        public async Task AddExternalUserIntoUserAsync(ExternalUser externalUser, Guid userUid) =>
            await _users.UpdateOneAsync(Builders<DbUser>.Filter.Eq(v => v.Id, userUid.ToString()), 
                Builders<DbUser>.Update.AddToSet(items => items.ExternalUsers, externalUser.ToDbExternalUser()), 
                new UpdateOptions() { IsUpsert = true });

        /// <summary>
        /// Adds new user async
        /// </summary> 
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddUserAsync(User user)
        {
            await _users.InsertOneAsync(user.ToDbUser());
        }

        /// <summary>
        /// Updates user async
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> UpdateUserAsync(User user)
        {
            var dbUser = (await _users.FindSync(Builders<DbUser>.Filter.Eq(d => d.Id, user.Id.ToString()), 
                new FindOptions<DbUser> { Limit = 1 }).ToListAsync()).GetOne();

            if (!user.HashPassword.IsNullOrEmpty())
            {
                dbUser.HashPassword = user.HashPassword;
            }
            if (!user.Salt.IsNullOrEmpty())
            {
                dbUser.Salt = user.Salt;
            }
            if (!user.PhoneNumber.IsNullOrEmpty())
            {
                dbUser.PhoneNumber = user.PhoneNumber;
            }
            if (!user.PhotoUrl.IsNullOrEmpty())
            {
                dbUser.PhotoUrl = user.PhotoUrl;
            }
            dbUser.DefaultLanguage = user.Language;

            return (await _users.FindOneAndUpdateAsync(Builders<DbUser>.Filter.Where(u => u.Id == user.Id.ToString()),
                Builders<DbUser>.Update
                    .Set(f => f.HashPassword, dbUser.HashPassword)
                    .Set(f => f.Salt, dbUser.Salt)
                    .Set(f => f.PhoneNumber, dbUser.PhoneNumber)
                    .Set(f => f.DefaultLanguage, dbUser.DefaultLanguage)
                    .Set(f => f.PhotoUrl, dbUser.PhotoUrl),
                new FindOneAndUpdateOptions<DbUser>
                {
                    ReturnDocument = ReturnDocument.After
                })).ToUser();
        }

        /// <summary>
        /// Get external users by email async
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<AllowedExternalUser> GetExternalUserByEmailAsync(string mail)
        {
            var list = await _allowedExternalUser.FindSync(Builders<DbAllowedExternalUser>.Filter.Eq(e => e.Mail, mail)).ToListAsync();
            
             return list.GetOne().ToAllowedExternalUser();
        }

        /// <summary>
        /// Gets user by email.
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmailAsync(string mail) =>
            await GetUserAsync(Builders<DbUser>.Filter.Eq(d => d.Mail, mail));

        /// <summary>
        /// Gets user by external user async
        /// </summary>
        /// <param name="externalUser"></param>
        /// <returns></returns>
        public async Task<User> GetUserByExternalUserAsync(ExternalUser externalUser)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            stringBuilder.Append("\"externalUsers.identifier\": ");
            stringBuilder.Append($"\"{externalUser.Identifier}\", ");
            stringBuilder.Append("\"externalUsers.provider\": ");
            stringBuilder.Append($"{(int)externalUser.Provider} ");
            stringBuilder.Append("}");
            return await GetUserAsync(stringBuilder.ToString());
        }
            
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
