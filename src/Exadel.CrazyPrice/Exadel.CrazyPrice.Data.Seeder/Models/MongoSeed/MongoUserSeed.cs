using Exadel.CrazyPrice.Data.Indexes;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Data;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models.MongoSeed
{
    public class MongoUserSeed : MongoAbstractSeed<DbUser>
    {
        public MongoUserSeed(SeederConfiguration configuration) : base(configuration)
        {
            CollectionName = "Users";
            IndexModels = DbUserIndexes.GetIndexes;
            DefaultCountSeed = configuration.DefaultCountSeed;

            var client = new MongoClient(configuration.ConnectionString);
            var db = client.GetDatabase(configuration.Database);

            Collection = db.GetCollection<DbUser>(CollectionName);
        }

        public override async Task SeedAsync()
        {
            var users = FakeData.Users;

            await base.SeedAsync();

            foreach (var dbUser in users)
            {
                await Collection.UpdateOneAsync(
                    u => u.Id == dbUser.Id,
                    Builders<DbUser>.Update
                        .Set(f => f.Name, dbUser.Name)
                        .Set(f => f.Mail, dbUser.Mail)
                        .Set(f => f.Surname, dbUser.Surname)
                        .Set(f => f.PhoneNumber, dbUser.PhoneNumber)
                        .Set(f => f.HashPassword, dbUser.HashPassword)
                        .Set(f => f.Salt, dbUser.Salt)
                        .Set(f => f.Roles, dbUser.Roles),
                    new UpdateOptions { IsUpsert = true });
            }

            await TimerDisposeAsync();
        }
    }
}