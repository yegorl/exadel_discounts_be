using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Data.Models;
using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Data.Extentions
{
    /// <summary>
    /// Represents extentions for DbUser.
    /// </summary>
    public static class DbUserExtentions
    {
        /// <summary>
        /// Gets one User from user list.
        /// </summary>
        /// <param name="dbUsers"></param>
        /// <returns></returns>
        public static DbUser GetOne(this List<DbUser> dbUsers)
        {
            return dbUsers == null || dbUsers.Count == 0 ? new DbUser() : dbUsers[0];
        }

        /// <summary>
        /// Gets the User entity from DbUser entity.
        /// </summary>
        /// <param name="dbUser"></param>
        /// <returns></returns>
        public static User ToUser(this DbUser dbUser)
        {
            try
            {
                return new User
                {
                    Id = new Guid(dbUser.Id),
                    Name = dbUser.Name,
                    Surname = dbUser.Surname,
                    PhoneNumber = dbUser.PhoneNumber,
                    Mail = dbUser.Mail,
                    HashPassword = dbUser.HashPassword,
                    Salt = dbUser.Salt,
                    Roles = dbUser.Roles,
                    ExternalUsers = dbUser.ExternalUsers.ToExternalUsers()
                };
            }
            catch
            {
                return new User();
            }
        }

        /// <summary>
        /// Gets the DbUser entity from User entity.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static DbUser ToDbUser(this User user) =>
            user == null || user.Id == Guid.Empty
                ? null
                : new DbUser
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Surname = user.Surname,
                    PhoneNumber = user.PhoneNumber,
                    Mail = user.Mail,
                    Roles = user.Roles,
                    HashPassword = user.HashPassword,
                    Salt = user.Salt
                };

        public static List<ExternalUser> ToExternalUsers(this List<DbExternalUser> dbExternalUsers)
        {
            var externalUsers = new List<ExternalUser>();
            foreach (var dbExternalUser in dbExternalUsers)
            {
                externalUsers.Add(new ExternalUser
                {
                    UserId = dbExternalUser.UserId,
                    ProviderName = dbExternalUser.ProviderName,
                    Mail = dbExternalUser.Mail,
                    Name = dbExternalUser.Name,
                    Surname = dbExternalUser.Surname
                });
            }

            return externalUsers;
        }
    }
}
