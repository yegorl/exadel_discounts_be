namespace Exadel.CrazyPrice.IdentityServer.Interfaces
{
    public interface ICryptographicService
    {
        /// <summary>
        /// Checking if password and hash match after password hashing
        /// </summary>
        /// <param name="password">password</param>
        /// <param name="hashPassword">Hashed password</param>
        /// <param name="salt">Salt</param>
        bool ComparePasswordHash(string password, string hashPassword, string salt);
    }
}
