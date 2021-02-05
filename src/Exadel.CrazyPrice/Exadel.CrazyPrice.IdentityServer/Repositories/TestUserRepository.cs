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
        public Task<User> GetUserByUidAsync(Guid uid)
        {
            return Task.FromResult(_testUsers.FirstOrDefault(u => u.Id == uid));
        }

        public Task<User> GetUserByEmailAsync(string mail)
        {
            return Task.FromResult(_testUsers.FirstOrDefault(u => u.Mail == mail));
        }
    }
}
