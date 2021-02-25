using Exadel.CrazyPrice.Data.Indexes;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Data;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models.MongoSeed
{
    public class MongoExternalUserSeed : MongoAbstractSeed<DbExternalUser>
    {
        public MongoExternalUserSeed(SeederConfiguration configuration) : base(configuration)
        {
            CollectionName = "ExternalUsers";
            IndexModels = DbExternalUserIndexes.GetIndexes;
            DefaultCountSeed = configuration.DefaultCountSeed;

            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);

            Collection = db.GetCollection<DbExternalUser>(CollectionName);
        }

        public override async Task SeedAsync()
        {
            var users = FakeData.ExternalUsers;

            await base.SeedAsync();

            foreach (var user in users)
            {
                await Collection.UpdateOneAsync(
                    u => u.Id == user.Id,
                    Builders<DbExternalUser>.Update
                        .Set(f => f.Mail, user.Mail)
                        .Set(f => f.Roles, user.Roles),
                    new UpdateOptions { IsUpsert = true });
            }

            await TimerDisposeAsync();
        }
    }
}