using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Data.Indexes;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using MongoDB.Driver;

namespace Exadel.CrazyPrice.Data.Seeder.Models.MongoSeed
{
    public class MongoTagSeed : MongoAbstractSeed<DbTag>
    {
        private readonly IMongoCollection<DbDiscount> _discounts;

        public MongoTagSeed(SeederConfiguration configuration) : base(configuration)
        {
            CollectionName = "Tags";
            IndexModels = DbTagIndexes.GetIndexes;

            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);

            Collection = db.GetCollection<DbTag>(CollectionName);
            _discounts = db.GetCollection<DbDiscount>("Discounts");
        }

        public override async Task SeedAsync()
        {
            await base.SeedAsync();

            await UpsertTags("tags",
                tag => Builders<DbTag>.Update
                    .Set(f => f.Name, tag)
                    .Set(f => f.Language, "russian"));

            await UpsertTags("translations.tags",
                tag => Builders<DbTag>.Update
                    .Set(f => f.Name, tag)
                    .Set(f => f.Translations, new List<DbTag>
                    {
                        new()
                        {
                            Name = tag,
                            Language = "english"
                        }
                    }));

            await TimerDisposeAsync();
        }

        private async Task UpsertTags(string field, Func<string, UpdateDefinition<DbTag>> updateDefinition)
        {
            var filter = "{$and : [{\"" + field + "\" : {$exists : true } },{\"" + field + "\" : {$ne : null } }] }";
            
            using var cursor = await _discounts.DistinctAsync<string>(field, filter);
            while (await cursor.MoveNextAsync())
            {
                var tags = cursor.Current;
                foreach (var tagString in tags)
                {
                    var update = updateDefinition(tagString);
                    await Collection.UpdateOneAsync(dbTag => dbTag.Name == tagString, update, new UpdateOptions { IsUpsert = true });
                }
            }
        }
    }
}