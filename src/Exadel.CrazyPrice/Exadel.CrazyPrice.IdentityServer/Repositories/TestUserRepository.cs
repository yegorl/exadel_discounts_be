using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Models;
using IdentityServer4.Test;

namespace Exadel.CrazyPrice.IdentityServer.Repositories
{
    public class TestUserRepository : IUserRepository
    {
        private readonly IEnumerable<CustomUser> _testUsers;

        public TestUserRepository()
        {
            _testUsers = CustomTestUsers.Users;
        }
        public Task<CustomUser> GetUserByEmail(string email)
        {
            return Task.FromResult(_testUsers.FirstOrDefault(u => u.Email == email));
        }

        public Task<CustomUser> GetUserByUid(string userUid)
        {
            var giud = new Guid(userUid);
            return Task.FromResult(_testUsers.FirstOrDefault(u => u.SubjectUid == giud));
        }
    }
}
