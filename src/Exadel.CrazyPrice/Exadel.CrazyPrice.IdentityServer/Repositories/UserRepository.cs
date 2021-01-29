using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Models;

namespace Exadel.CrazyPrice.IdentityServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IEnumerable<CustomUser> _users;
        public UserRepository()
        {
            _users = CustomTestUsers.Users;
        }

        public async Task<bool> ValidateCredentials(string email, string password)
        {
            var user = await GetUserByEmail(email);

            if (user != null)
            {
                if (string.IsNullOrWhiteSpace(user.Password) && string.IsNullOrWhiteSpace(password))
                {
                    return true;
                }
                return user.Password.Equals(password);
            }
            return false;
        }

        public async Task<CustomUser> GetUserByEmail(string email)
        {
            return await Task.FromResult(_users.FirstOrDefault(x => x.Email == email));
        }

        public async Task<CustomUser> GetUserByUid(string userId)
        {
            return await Task.FromResult(_users.FirstOrDefault(x => x.SubjectId == userId));
        }
    }
}
