using Exadel.CrazyPrice.Data.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Data.Indexes
{
    /// <summary>
    /// Determines external user indexes.
    /// </summary>
    public static class DbAllowedExternalUserIndexes
    {
        /// <summary>
        /// Gets external user indexes.
        /// </summary>
        public static List<CreateIndexModel<DbAllowedExternalUser>> GetIndexes => new()
        {
            new CreateIndexModel<DbAllowedExternalUser>(Builders<DbAllowedExternalUser>
                            .IndexKeys
                            .Text(c => c.Mail),
                                new CreateIndexOptions
                                {
                                    Name = "idx_text"
                                }),

            new CreateIndexModel<DbAllowedExternalUser>(Builders<DbAllowedExternalUser>.IndexKeys.Ascending(c => c.Mail))
        };
    }
}
