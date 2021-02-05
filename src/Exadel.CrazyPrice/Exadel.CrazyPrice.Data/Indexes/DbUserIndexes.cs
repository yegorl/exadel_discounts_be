using Exadel.CrazyPrice.Data.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Data.Indexes
{
    public static class DbUserIndexes
    {
        public static List<CreateIndexModel<DbUser>> GetIndexes => new()
        {
            new CreateIndexModel<DbUser>(Builders<DbUser>
                            .IndexKeys
                            .Text(c => c.Name)
                            .Text(c => c.Surname)
                            .Text(c => c.Mail),
                                new CreateIndexOptions
                                {
                                    Name = "idx_text"
                                }),

            new CreateIndexModel<DbUser>(Builders<DbUser>.IndexKeys.Ascending(c => c.Name)),

            new CreateIndexModel<DbUser>(Builders<DbUser>.IndexKeys.Ascending(c => c.Surname)),

            new CreateIndexModel<DbUser>(Builders<DbUser>.IndexKeys.Ascending(c => c.Mail)),

            new CreateIndexModel<DbUser>(Builders<DbUser>.IndexKeys.Ascending(c => c.PhoneNumber)),

            new CreateIndexModel<DbUser>(Builders<DbUser>.IndexKeys.Ascending(c => c.Roles))
        };
    }
}
