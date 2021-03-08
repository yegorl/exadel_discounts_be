using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Services.Common.Cryptography.Extentions
{
    public static class StringExtensions
    {
        private const int HashSize = 128;
        private const int SaltSize = 30;
        private const int IterationCount = 20;


        public static (string hashPassword, string salt) GetCryptPassword(this string password)
        {
            var salt = new byte[SaltSize];
            byte[] hash;

            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(salt);

            //generate hash from test password and original salt and iterations
            using (var rfc2898DeriveBytes =
                new Rfc2898DeriveBytes(password, salt, IterationCount, HashAlgorithmName.SHA256))
            {
                hash = rfc2898DeriveBytes.GetBytes(HashSize);
            }

            return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
        }


        public static bool IsValidPasswordHash(this string password, string hashedPassword, string salt)
        {
            var saltByte = Convert.FromBase64String(salt);
            var result = false;
            byte[] testHash;

            //generate hash from test password and original salt and iterations
            using (var rfc2898DeriveBytes =
                new Rfc2898DeriveBytes(password, saltByte, IterationCount, HashAlgorithmName.SHA256))
            {
                testHash = rfc2898DeriveBytes.GetBytes(HashSize);
            }

            try
            {
                result = StructuralComparisons.StructuralEqualityComparer.Equals(
                    Convert.FromBase64String(hashedPassword), testHash);
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
