using Exadel.CrazyPrice.Data.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Data.Indexes
{
    public static class DbTagIndexes
    {
        public static List<CreateIndexModel<DbTag>> GetIndexes => new()
        {
            new CreateIndexModel<DbTag>(Builders<DbTag>
                            .IndexKeys
                            .Text(c => c.Name)
                            .Text("translations.name"),
                             new CreateIndexOptions
                             {
                                 Name = "idx_text"
                             }),

            //new CreateIndexModel<DbTag>(Builders<DbTag>.IndexKeys.Ascending(c => c.Name), new CreateIndexOptions { Unique = true }),

            new CreateIndexModel<DbTag>(Builders<DbTag>.IndexKeys.Ascending(new StringFieldDefinition<DbTag>("translations.name"))),
        };
    }
}
