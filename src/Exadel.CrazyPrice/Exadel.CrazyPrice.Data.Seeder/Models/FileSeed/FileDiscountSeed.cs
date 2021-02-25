using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Seeder.Configuration;
using Exadel.CrazyPrice.Data.Seeder.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Transactions;

namespace Exadel.CrazyPrice.Data.Seeder.Models.FileSeed
{
    public class FileDiscountSeed : FileAbstractSeed<DbTag>
    {
        private long _counter;

        public FileDiscountSeed(SeederConfiguration configuration) : base(configuration)
        {
            CollectionName = "Discounts.json";
            DefaultCountSeed = configuration.DefaultCountSeed;
        }

        public override async Task SeedAsync()
        {
            uint countForInsert = 500;

            if (DefaultCountSeed == 0)
            {
                return;
            }

            var count = Math.Truncate((double)DefaultCountSeed / countForInsert);
            var countPart = DefaultCountSeed - (uint)count * countForInsert;

            await base.SeedAsync();

            try
            {
                await File.AppendAllTextAsync(FullName, "[");

                if (DefaultCountSeed > countForInsert)
                {
                    for (var i = 0; i < count; i++)
                    {
                        await InsertAsync(countForInsert);
                    }

                    if (countPart > 0)
                    {
                        await InsertAsync(countPart);
                    }
                }
                else
                {
                    await InsertAsync(DefaultCountSeed);
                }

                ChangeSymbol(FullName);
            }
            catch (Exception ex)
            {
                throw new Exception($"File write error: {ex.Message}");
            }
            await TimerDisposeAsync();
        }

        protected override async Task<long> CountEstimatedAsync()
        {
            return _counter;
        }

        private async Task InsertAsync(uint count)
        {
            var geoCountries = FakeData.GeoCountries;

            if (count < geoCountries.Count)
            {
                geoCountries = geoCountries.GetRange(0, Convert.ToInt32(count));
            }

            var iteration = Math.Truncate((double)count / geoCountries.Count);

            var rest = count - iteration * geoCountries.Count;

            foreach (var geoCountry in geoCountries)
            {
                await InsertAsync(Convert.ToUInt32(iteration), FakeData.UsersId, geoCountry);
            }

            if (rest > 0)
            {
                await InsertAsync(Convert.ToUInt32(rest), FakeData.UsersId, geoCountries[0]);
            }
        }

        private async Task InsertAsync(uint count, List<string> usersId, GeoCountry geo)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };

            var discountData = new FakeDiscounts().Get(count, usersId, geo);

            foreach (var dbDiscount in discountData)
            {
                var value = JsonSerializer.Serialize(dbDiscount, jsonSerializerOptions).Replace("\"id\":", "\"_id\":") + ",";
                await File.AppendAllTextAsync(FullName, value);
                _counter++;
            }
        }

        public static void ChangeSymbol(string fullName)
        {
            using (var fs = new FileStream(fullName, FileMode.Open))
            {
                fs.Position = fs.Seek(-1, SeekOrigin.End);

                if (fs.ReadByte() != ',')
                {
                    return;
                }

                fs.Position = fs.Seek(-1, SeekOrigin.End);
                fs.WriteByte(Convert.ToByte(']'));
            }
        }
    }
}