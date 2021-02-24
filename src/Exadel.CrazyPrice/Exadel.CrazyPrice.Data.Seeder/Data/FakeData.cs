using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Seeder.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Exadel.CrazyPrice.Data.Seeder.Data
{
    public static class FakeData
    {
        public static List<string> UsersId =>
            new()
            {
                "a3cf9296-9e83-410c-8341-f4d36df28b46",
                "700f5bbb-ac24-4e48-9add-0f430f882481",
                "48022eab-fed0-46f9-93af-3a1478ca6b64",
                "4e3a6563-848b-4fb9-9b37-2ce63d29763d",
                "bea4c97c-4e8f-429d-a95e-6d6601a35db5",
                "38697f47-e410-4456-bb5c-4fed0b6c1bb1",
                "d412793b-ee1c-48bc-9428-d1735558ec33",
                "4fa26daf-5f7d-4e97-84ce-648f1d93fad4",
                "9436b060-85cf-4af4-8f1e-a9f550ea3bd9",
                "b3a6ecae-60ce-4f9f-b152-fb15f0ddee3c",
                "58692fd8-3b85-478d-9f06-19747cc4ea24",
                "3c9b07f6-4546-4abb-80c6-49e8732aac4b",
                "9332ab9d-c0d1-434b-aada-7c9e8cf02f4c",
                "2ec19f55-bd3b-4f06-b97d-265a5b067167",
                "e5ed6a89-266d-403b-b8f6-5e61c181a016"
            };

        public static List<GeoCountry> GeoCountries =>
            new List<GeoCountry>()
            {
                new ()
                {
                    CountryRu = "Беларусь",
                    CityRu = "Минск",
                    CountryEn = "Belarus",
                    CityEn = "Minsk"
                },
                new ()
                {
                    CountryRu = "Беларусь",
                    CityRu = "Гродно",
                    CountryEn = "Belarus",
                    CityEn = "Grodno"
                },
                new ()
                {
                    CountryRu = "Беларусь",
                    CityRu = "Гомель",
                    CountryEn = "Belarus",
                    CityEn = "Gomel"
                },
                new ()
                {
                    CountryRu = "Беларусь",
                    CityRu = "Витебск",
                    CountryEn = "Belarus",
                    CityEn = "Vitebsk"
                },
                new ()
                {
                    CountryRu = "Польша",
                    CityRu = "Беласток",
                    CountryEn = "Poland",
                    CityEn = "Bialystok"
                },
                new ()
                {
                    CountryRu = "Польша",
                    CityRu = "Варшава",
                    CountryEn = "Poland",
                    CityEn = "Warsaw"
                },
                new ()
                {
                    CountryRu = "Литва",
                    CityRu = "Вильнюс",
                    CountryEn = "Lithuania",
                    CityEn = "Vilnius"
                },
                new ()
                {
                    CountryRu = "Литва",
                    CityRu = "Клайпеда",
                    CountryEn = "Lithuania",
                    CityEn = "Klaipeda"
                },
                new ()
                {
                    CountryRu = "Украина",
                    CityRu = "Львов",
                    CountryEn = "Ukraine",
                    CityEn = "Lviv"
                },
                new ()
                {
                    CountryRu = "Украина",
                    CityRu = "Одесса",
                    CountryEn = "Ukraine",
                    CityEn = "Odessa"
                },
                new ()
                {
                    CountryRu = "Украина",
                    CityRu = "Харьков",
                    CountryEn = "Ukraine",
                    CityEn = "Kharkiv"
                },new ()
                {
                    CountryRu = "Украина",
                    CityRu = "Винница",
                    CountryEn = "Ukraine",
                    CityEn = "Vinnitsa"
                },
                new ()
                {
                    CountryRu = "Узбекистан",
                    CityRu = "Ташкент",
                    CountryEn = "Uzbekistan",
                    CityEn = "Tashkent"
                },
            };

        public static List<DbUser> Users => new List<DbUser>
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
                Roles = RoleOption.Employee,
                Type = UserTypeOption.Internal,
                Provider = ProviderOptions.None

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
                Roles = RoleOption.Moderator,
                Type = UserTypeOption.Internal,
                Provider = ProviderOptions.None
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
                Roles = RoleOption.Administrator,
                Type = UserTypeOption.Internal,
                Provider = ProviderOptions.None
            }
        };

        public static List<DbTagFake> Tags => JsonSerializer.Deserialize<List<DbTagFake>>("[{\"_id\":\"Автомобильное\",\"language\":\"russian\"},{\"_id\":\"Бакалея\",\"language\":\"russian\"},{\"_id\":\"Галантерея\",\"language\":\"russian\"},{\"_id\":\"Дом\",\"language\":\"russian\"},{\"_id\":\"Игрушки\",\"language\":\"russian\"},{\"_id\":\"Книги\",\"language\":\"russian\"},{\"_id\":\"Меха\",\"language\":\"russian\"},{\"_id\":\"Одежда\",\"language\":\"russian\"},{\"_id\":\"Пряжа\",\"language\":\"russian\"},{\"_id\":\"Спорт\",\"language\":\"russian\"},{\"_id\":\"Фильмы\",\"language\":\"russian\"},{\"_id\":\"Электроника\",\"language\":\"russian\"},{\"_id\":\"детское\",\"language\":\"russian\"},{\"_id\":\"для малышей\",\"language\":\"russian\"},{\"_id\":\"здоровье\",\"language\":\"russian\"},{\"_id\":\"игры\",\"language\":\"russian\"},{\"_id\":\"компьютеры\",\"language\":\"russian\"},{\"_id\":\"красота\",\"language\":\"russian\"},{\"_id\":\"музыка\",\"language\":\"russian\"},{\"_id\":\"обувь\",\"language\":\"russian\"},{\"_id\":\"промышленное\",\"language\":\"russian\"},{\"_id\":\"садинструмент\",\"language\":\"russian\"},{\"_id\":\"туризм\",\"language\":\"russian\"},{\"_id\":\"украшения\",\"language\":\"russian\"},{\"_id\":\"Automotive\",\"translations\":[{\"_id\":\"Automotive\",\"language\":\"english\"}]},{\"_id\":\"Baby\",\"translations\":[{\"_id\":\"Baby\",\"language\":\"english\"}]},{\"_id\":\"Beauty\",\"translations\":[{\"_id\":\"Beauty\",\"language\":\"english\"}]},{\"_id\":\"Books\",\"translations\":[{\"_id\":\"Books\",\"language\":\"english\"}]},{\"_id\":\"Clothing\",\"translations\":[{\"_id\":\"Clothing\",\"language\":\"english\"}]},{\"_id\":\"Computers\",\"translations\":[{\"_id\":\"Computers\",\"language\":\"english\"}]},{\"_id\":\"Electronics\",\"translations\":[{\"_id\":\"Electronics\",\"language\":\"english\"}]},{\"_id\":\"Games\",\"translations\":[{\"_id\":\"Games\",\"language\":\"english\"}]},{\"_id\":\"Garden\",\"translations\":[{\"_id\":\"Garden\",\"language\":\"english\"}]},{\"_id\":\"Grocery\",\"translations\":[{\"_id\":\"Grocery\",\"language\":\"english\"}]},{\"_id\":\"Health\",\"translations\":[{\"_id\":\"Health\",\"language\":\"english\"}]},{\"_id\":\"Home\",\"translations\":[{\"_id\":\"Home\",\"language\":\"english\"}]},{\"_id\":\"Industrial\",\"translations\":[{\"_id\":\"Industrial\",\"language\":\"english\"}]},{\"_id\":\"Jewelery\",\"translations\":[{\"_id\":\"Jewelery\",\"language\":\"english\"}]},{\"_id\":\"Kids\",\"translations\":[{\"_id\":\"Kids\",\"language\":\"english\"}]},{\"_id\":\"Movies\",\"translations\":[{\"_id\":\"Movies\",\"language\":\"english\"}]},{\"_id\":\"Music\",\"translations\":[{\"_id\":\"Music\",\"language\":\"english\"}]},{\"_id\":\"Outdoors\",\"translations\":[{\"_id\":\"Outdoors\",\"language\":\"english\"}]},{\"_id\":\"Shoes\",\"translations\":[{\"_id\":\"Shoes\",\"language\":\"english\"}]},{\"_id\":\"Sports\",\"translations\":[{\"_id\":\"Sports\",\"language\":\"english\"}]},{\"_id\":\"Tools\",\"translations\":[{\"_id\":\"Tools\",\"language\":\"english\"}]},{\"_id\":\"Toys\",\"translations\":[{\"_id\":\"Toys\",\"language\":\"english\"}]}]"
        //, new JsonSerializerOptions?()
        //{
        //    Encoder = 
        //}
        );
    }

    public class DbTagFake
    {
        [JsonPropertyName("_id")]
        public string Name { get; set; }

        [JsonPropertyName("language")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Language { get; set; }

        [JsonPropertyName("translations")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<DbTagFake> Translations { get; set; }
    }
}