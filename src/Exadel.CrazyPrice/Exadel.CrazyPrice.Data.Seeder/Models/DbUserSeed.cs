using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Data.Indexes;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models
{
    public class DbUserSeed : AbstractSeed<DbUser>
    {
        public DbUserSeed(SeederConfiguration configuration) : base(configuration)
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
            var users =
                new List<DbUser>
                {
                    // password: 1111
                    new()
                    {
                        Id = "97b248a1-c706-4987-9b31-101429ec2aff",
                        Name = "Frank",
                        Mail = "frank@gmail.com",
                        Surname = "Habot",
                        PhoneNumber = "375129802178",
                        HashPassword =
                            "+SzcAYCGZcD+eov22e4GpybKoLsaoguAIPS0D3Ntj2ATCpcQvvbsitFLFT1IZOTEzjvxTOQ/ISdxSW9x+VmtV1UFTGgIgKPZPKMtkWmyL7SOza6iuFbikXRr6olGNkdRK6KEOSyY0d+S1VZ9RbcGl9eYfppPM5s8xVc7w0FsODw=",
                        Salt = "IZmh6o3UgDIHs5rXD3L5hD2momUnLQdyJUrpQNW/",
                        Roles = RoleOption.Employee
                    },
                    // password: 2222
                    new()
                    {
                        Id = "13b4e6a3-42f2-4a43-be4e-7fc15454a7ab",
                        Name = "Claire",
                        Mail = "claire@gmail.com",
                        Surname = "Flower",
                        PhoneNumber = "375189672970",
                        HashPassword =
                            "+fs8G3LGPEg3HFHyJNfv4jAe+a9zKCj4AgZDBVatsGcHW625Ce+QO6cTcI/oLFn2ArUvvYPs7Hs664OPojKm3A6kPAQ77ysqzigxg75xbKS2Cel5Un7BaIBMN+BFRU5CnaXnPQ4rmOENf8p70FbdwWr359pvmTttFWsQAG2iCjI=",
                        Salt = "sOVqvk4NWiJ4gbTeVJ19M3V2gG5jOdootBQbTv6v",
                        Roles = RoleOption.Moderator
                    },
                    // password: 3333
                    new()
                    {
                        Id = "10c1a0a1-556a-475f-9597-c705dc1cc48b",
                        Name = "Bob",
                        Mail = "bob@gmail.com",
                        Surname = "Norris",
                        PhoneNumber = "375170807175",
                        HashPassword =
                            "voWfJR7QxiT3sTuLZMu+iuYswOika7FU+VTtRhATkhSdzznn7pOnSH1VEsZbNlqWOaRpvTskIlBUmvXwct5KZ94cg3T93dVLmSCenh8VjPmFKTiGHxP+dboJLXjeKQ6BUNoNwn3w5v16OFRH+QgGesrdkWsLi2V5QQ/+BO9VDVA=",
                        Salt = "wQFhWPjNcYukdImNixjiATqeQLemqaJA55jQkfgg",
                        Roles = RoleOption.Administrator
                    }
                };

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