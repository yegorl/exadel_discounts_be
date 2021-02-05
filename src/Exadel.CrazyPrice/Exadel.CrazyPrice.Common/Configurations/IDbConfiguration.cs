namespace Exadel.CrazyPrice.Common.Configurations
{
    /// <summary>
    /// Determines IDbConfiguration. 
    /// </summary>
    public interface IDbConfiguration
    {
        /// <summary>
        /// Provides connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Provides a Database name when Db client (driver) is not support the Database name from connection string.
        /// Like MongoDB.Driver.
        /// </summary>
        string Database { get; }
    }
}
