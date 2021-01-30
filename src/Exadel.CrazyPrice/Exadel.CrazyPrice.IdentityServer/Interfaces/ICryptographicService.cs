using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.IdentityServer.Interfaces
{
    public interface ICryptographicService
    {
        /// <summary>
        /// Generate hashed string
        /// </summary>
        /// <param name="password">The password to be hashed</param>
        /// <param name="iterations">Count of iterations</param>
        /// <returns>Hashed string </returns>
        string GenerateHash(string password, int iterations = 8312);
        /// <summary>
        /// Checking if password and hash match after password hashing
        /// </summary>
        /// <param name="password">password</param>
        /// <param name="hash">Hashed password</param>
        bool IsValid(string password, string hash);

    }
}
