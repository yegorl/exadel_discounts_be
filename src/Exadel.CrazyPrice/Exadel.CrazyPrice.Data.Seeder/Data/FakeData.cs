using Exadel.CrazyPrice.Data.Seeder.Models;
using System.Collections.Generic;

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
    }
}