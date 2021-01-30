using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Microsoft.VisualBasic.CompilerServices;

namespace Exadel.CrazyPrice.IdentityServer.Services
{
    public class CryptographicService : ICryptographicService
    {
        public string GenerateHash(string password, int iterations = 8312)
        {
            var salt = new byte[24];
            byte[] hash;
            //generate a random salt for hashing
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            //generate hash from password and salt and iterations
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                hash = rfc2898DeriveBytes.GetBytes(24);
            }

            //return delimited string with salt | #iterations | hash
            return Convert.ToBase64String(salt) + "|" + iterations + "|" + Convert.ToBase64String(hash);
        }

        public bool IsValid(string password, string hashedPassword)
        {
            var splitHashPassword = hashedPassword.Split('|');
            var salt = Convert.FromBase64String(splitHashPassword[0]);
            var iterations = Int32.Parse(splitHashPassword[1]);
            var hash = splitHashPassword[2];
            byte[] testHash;

            //generate hash from test password and original salt and iterations
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                testHash = rfc2898DeriveBytes.GetBytes(24);
            }

            //if hash values match then return success
            if (Convert.ToBase64String(testHash) == hash)
                return true;

            //no match return false
            return false;
        }
    }
}
