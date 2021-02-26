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
        /// Gets one User from user list.
        /// </summary>
        /// <param name="dbExternalUsers"></param>
        /// <returns></returns>
        public static DbAllowedExternalUser GetOne(this List<DbAllowedExternalUser> dbExternalUsers)
        {
            return dbExternalUsers == null || dbExternalUsers.Count == 0 ? new DbAllowedExternalUser() : dbExternalUsers[0];
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
        /// Gets the User entity from DbUser entity.
        /// </summary>
        /// <param name="dbUser"></param>
        /// <returns></returns>
        public static AllowedExternalUser ToAllowedExternalUser(this DbAllowedExternalUser dbUser)
        {
            try
            {
                return new AllowedExternalUser
                {
                    Id = new Guid(dbUser.Id),
                    Mail = dbUser.Mail,
                    Roles = dbUser.Roles
                };
            }
            catch
            {
                return new AllowedExternalUser();
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
                    Salt = user.Salt,
                    ExternalUsers = user.ExternalUsers.ToDbExternalUsers()
                };

        public static List<ExternalUser> ToExternalUsers(this List<DbExternalUser> dbExternalUsers)
        {
            var externalUsers = new List<ExternalUser>();
            foreach (var dbExternalUser in dbExternalUsers)
            {
                externalUsers.Add(ToExternalUser(dbExternalUser));
            }

            return externalUsers;
        }

        public static List<DbExternalUser> ToDbExternalUsers(this List<ExternalUser> externalUsers)
        {
            var dbExternalUsers = new List<DbExternalUser>();
            foreach (var externalUser in externalUsers)
            {
                dbExternalUsers.Add(ToDbExternalUser(externalUser));
            }

            return dbExternalUsers;
        }

        public static DbExternalUser ToDbExternalUser(this ExternalUser externalUser) =>
            new() { Provider = externalUser.Provider, Identifier = externalUser.Identifier };

        public static ExternalUser ToExternalUser(this DbExternalUser dbExternalUser) =>
            new() { Provider = dbExternalUser.Provider, Identifier = dbExternalUser.Identifier };
    }
}
