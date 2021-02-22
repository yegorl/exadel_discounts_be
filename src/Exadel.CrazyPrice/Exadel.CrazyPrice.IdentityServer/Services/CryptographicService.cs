using Exadel.CrazyPrice.IdentityServer.Interfaces;
using System;
using System.Security.Cryptography;

namespace Exadel.CrazyPrice.IdentityServer.Services
{
    public class CryptographicService : ICryptographicService
    {
        private const int SaltSize = 30;
        private const int HashSize = 128;
        private const int IterationCount = 20;
        public bool ComparePasswordHash(string password, string hashedPassword, string salt)
        {
            var saltByte = Convert.FromBase64String(salt);
            byte[] testHash;

            //generate hash from test password and original salt and iterations
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltByte, IterationCount, HashAlgorithmName.SHA256))
            {
                testHash = rfc2898DeriveBytes.GetBytes(HashSize);
            }

            //if hash values match then return success
            if (Convert.ToBase64String(testHash) == hashedPassword)
            {
                return true;
            }

            //no match return false
            return false;
        }
    }
}
