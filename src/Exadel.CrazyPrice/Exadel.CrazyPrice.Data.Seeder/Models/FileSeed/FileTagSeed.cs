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
    public class FileTagSeed : FileAbstractSeed<DbTagFake>
    {
        public FileTagSeed(SeederConfiguration configuration) : base(configuration)
        {
            CollectionName = "Tags.json";
            DefaultCountSeed = configuration.DefaultCountSeed;

            Collection = FakeData.Tags;
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
            });

            await File.WriteAllTextAsync(FullName, value);

            await TimerDisposeAsync();
        }
    }
}