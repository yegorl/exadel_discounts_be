using System;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.IdentityServer.Extentions;

namespace Exadel.CrazyPrice.IdentityServer.Services
{
    public class UserService : IUserService
    {
        private readonly ICryptographicService _cryptographicService;

        public UserService(ICryptographicService cryptographicService)
        {
            _cryptographicService = cryptographicService;
        }

        public bool ValidateCredentials(User user, string password)
        {
            return _cryptographicService.ComparePasswordHash(password, user.HashPassword, user.Salt);
        }
    }
}
