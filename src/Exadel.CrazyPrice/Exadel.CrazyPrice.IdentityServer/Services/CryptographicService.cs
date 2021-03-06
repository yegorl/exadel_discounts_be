using Exadel.CrazyPrice.IdentityServer.Interfaces;
using System;
using System.Security.Cryptography;
using Exadel.CrazyPrice.Services.Common.Cryptography.Extentions;

namespace Exadel.CrazyPrice.IdentityServer.Services
{
    public class CryptographicService : ICryptographicService
    {
        public bool ComparePasswordHash(string password, string hashedPassword, string salt) => 
            password.IsValidPasswordHash(hashedPassword, salt);
    }
}
