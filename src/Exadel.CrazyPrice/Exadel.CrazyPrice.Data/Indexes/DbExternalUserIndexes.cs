using Exadel.CrazyPrice.Data.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Data.Indexes
{
    /// <summary>
    /// Determines external user indexes.
    /// </summary>
    public static class DbExternalUserIndexes
    {
        /// <summary>
        /// Gets external user indexes.
        /// </summary>
        public static List<CreateIndexModel<DbExternalUser>> GetIndexes => new()
        {
            new CreateIndexModel<DbExternalUser>(Builders<DbExternalUser>
                            .IndexKeys
                            .Text(c => c.Mail),
                                new CreateIndexOptions
                                {
                                    Name = "idx_text"
                                }),

            new CreateIndexModel<DbExternalUser>(Builders<DbExternalUser>.IndexKeys.Ascending(c => c.Mail))
        };
    }
}
