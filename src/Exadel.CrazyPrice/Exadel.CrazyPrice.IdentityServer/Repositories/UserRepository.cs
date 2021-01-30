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
        private readonly ICryptographicService _cryptographicService;
        public UserRepository(ICryptographicService cryptographicService)
        {
            _users = CustomTestUsers.Users;
            _cryptographicService = cryptographicService;
        }

        public async Task<bool> ValidateCredentials(string email, string password)
        {
            var user = await GetUserByEmail(email);

            if (user != null)
            {
                return _cryptographicService.IsValid(password, user.Password);
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
