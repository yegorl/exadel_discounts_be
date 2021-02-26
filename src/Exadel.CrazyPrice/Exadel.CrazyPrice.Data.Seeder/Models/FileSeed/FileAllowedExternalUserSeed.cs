using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Data;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Data.Seeder.Models.FileSeed
{
    public class FileAllowedExternalUserSeed : FileAbstractSeed<DbAllowedExternalUser>
    {
        public FileAllowedExternalUserSeed(SeederConfiguration configuration) : base(configuration)
        {
            CollectionName = "AllowedExternalUsers.json";
            DefaultCountSeed = configuration.DefaultCountSeed;

            Collection = FakeData.AllowedExternalUsers;
        }

        public override async Task SeedAsync()
        {
            await base.SeedAsync();

            var value = JsonSerializer.Serialize(Collection, new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)

            }).Replace("\"id\":", "\"_id\":");

            await File.WriteAllTextAsync(FullName, value);

            await TimerDisposeAsync();
        }
    }
}