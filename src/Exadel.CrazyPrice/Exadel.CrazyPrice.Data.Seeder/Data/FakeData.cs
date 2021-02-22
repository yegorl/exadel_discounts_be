using Exadel.CrazyPrice.Data.Seeder.Models;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Data.Seeder.Data
{
    public static class FakeData
    {
        public static List<string> UsersId =>
            new()
            {
                "97b248a1-c706-4987-9b31-101429ec2aff",
                "13b4e6a3-42f2-4a43-be4e-7fc15454a7ab",
                "10c1a0a1-556a-475f-9597-c705dc1cc48b",
                "b3a6ecae-60ce-4f9f-b152-fb15f0ddee3c",
                "58692fd8-3b85-478d-9f06-19747cc4ea24",
                "3c9b07f6-4546-4abb-80c6-49e8732aac4b",
                "9332ab9d-c0d1-434b-aada-7c9e8cf02f4c",
                "2ec19f55-bd3b-4f06-b97d-265a5b067167",
                "e5ed6a89-266d-403b-b8f6-5e61c181a016",
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
    }
}