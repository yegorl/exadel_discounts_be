using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.IdentityServer.Repositories
{
    public class TestUserRepository : IUserRepository
    {
        private readonly IEnumerable<User> _testUsers;

        public TestUserRepository()
        {
            _testUsers = CustomTestUsers.Users;
        }
        public Task<User> GetUserByEmail(string email)
        {
            return Task.FromResult(_testUsers.FirstOrDefault(u => u.Mail == email));
        }

        public Task<User> GetUserByUid(string userUid)
        {
            var giud = new Guid(userUid);
            return Task.FromResult(_testUsers.FirstOrDefault(u => u.Id == giud));
        }

        public Task<User> GetUserByUidAsync(Guid uid)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAsync(string mail)
        {
            throw new NotImplementedException();
        }
    }
}
