using Bogus;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Models.Promocode;
using Exadel.CrazyPrice.Data.Seeder.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exadel.CrazyPrice.Data.Seeder
{
    public class FakeDiscounts
    {
        private class ValueString
        {
            public string Value { get; set; }
        }

        public IEnumerable<DbDiscount> Get(uint count, List<string> listUserId, GeoCountry geo)
        {

            var locationGenerator = new Faker<DbLocation>("ru")
                    .RuleFor(x => x.Latitude, f => f.Address.Latitude())
                    .RuleFor(x => x.Longitude, f => f.Address.Longitude())
                ;

            var dbLocation = locationGenerator.Generate();

            var companyGenerator = new Faker<DbCompany>("ru")
                    .RuleFor(x => x.Name, f => f.Company.CompanyName())
                    .RuleFor(x => x.Description, f => string.Join(" ", f.Commerce.Categories(15).Distinct().ToList()))
                    .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+!!! !! !!!-!!-!!"))
                    .RuleFor(x => x.Mail, f => f.Person.Email)
                ;

            var streetRu = new Faker<ValueString>("ru")
                .RuleFor(x => x.Value, x => x.Address.StreetAddress())
                .Generate();

            var addressRu = new DbAddress()
            {
                Country = geo.CountryRu,
                City = geo.CityRu,
                Street = streetRu.Value,
                Location = dbLocation
            }
                ;

            var dbUserGenerator = new Faker<DbUser>("ru")
                     .RuleFor(x => x.Id, f => Guid.NewGuid().ToString())
                     .RuleFor(x => x.Name, f => f.Person.FirstName)
                     .RuleFor(x => x.Surname, f => f.Person.LastName)
                     .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+!!! !! !!!-!!-!!"))
                     .RuleFor(x => x.Mail, f => f.Person.Email)
                 ;

            var companyTranslationGenerator = new Faker<DbCompany>("en")
                    .RuleFor(x => x.Name, f => f.Company.CompanyName())
                    .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(x => x.PhoneNumber, f => null)
                    .RuleFor(x => x.Mail, f => null)
                ;

            var streetEn = new Faker<ValueString>("en")
                .RuleFor(x => x.Value, x => x.Address.StreetAddress())
                .Generate();

            var addressEn = new DbAddress()
            {
                Country = geo.CountryEn,
                City = geo.CityEn,
                Street = streetEn.Value,
                Location = dbLocation
            }
                ;

            var translationGenerator = new Faker<DbTranslation>("en")
                    .RuleFor(x => x.Language, f => "english")
                    .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                    .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(x => x.Address, f => addressEn)
                    .RuleFor(x => x.Company, f => companyTranslationGenerator.Generate())
                    .RuleFor(x => x.Tags, f => f.Commerce.Categories(15).Distinct().ToList())
                ;

            var promocodeOptionGenerator = new Faker<DbPromocodeOptions>()
                    .RuleFor(x => x.CountActivePromocodePerUser, f => f.Random.Int(1, 5))
                    .RuleFor(x => x.CountSymbolsPromocode, f => f.Random.Int(4, 7))
                    .RuleFor(x => x.DaysDurationPromocode, f => f.Random.Int(5, 15))
                    .RuleFor(x => x.TimeLimitAddingInSeconds, f => f.Random.Int(1, 5))
                ;

            var discountGenerator = new Faker<DbDiscount>("ru")
                    .StrictMode(true)
                    .RuleFor(x => x.Id, f => Guid.NewGuid().ToString())
                    .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                    .RuleFor(x => x.Description, f => string.Join(" ", f.Commerce.Categories(15).Distinct().ToList()))
                    .RuleFor(x => x.AmountOfDiscount, f => f.Random.Int(500, 7000) / 100)
                    .RuleFor(x => x.StartDate, f => f.Date.Between(DateTime.Now - TimeSpan.FromDays(700), DateTime.Now))
                    .RuleFor(x => x.EndDate,
                        f => f.Date.Between(DateTime.Now + TimeSpan.FromDays(1), DateTime.Now + TimeSpan.FromDays(700)))
                    .RuleFor(x => x.Address, f => addressRu)
                    .RuleFor(x => x.Company, f => companyGenerator.Generate())
                    .RuleFor(x => x.PictureUrl, f => f.Image.LoremFlickrUrl())
                    .RuleFor(x => x.WorkingDaysOfTheWeek,
                        f => string.Join("", f.Random.Int(0, 1), f.Random.Int(0, 1), f.Random.Int(0, 1),
                            f.Random.Int(0, 1), f.Random.Int(0, 1), f.Random.Int(0, 1), f.Random.Int(0, 1)))
                    .RuleFor(x => x.Tags, f => f.Commerce.Categories(15).Distinct().ToList())
                    .RuleFor(x => x.RatingTotal, f => f.Random.Int(0, 4) + f.Random.Int(1, 9) / 10)
                    .RuleFor(x => x.ViewsTotal, f => f.Random.Int(0, 100))
                    .RuleFor(x => x.UsersSubscriptionTotal, f => f.Random.Int(0, 50))
                    .RuleFor(x => x.SubscriptionsTotal, f => f.Random.Int(0, 50))
                    .RuleFor(x => x.Language, f => "russian")
                    .RuleFor(x => x.Deleted, f => false)

                    .RuleFor(x => x.CreateDate,
                        f => f.Date.Between(DateTime.Now - TimeSpan.FromDays(700),
                            DateTime.Now - TimeSpan.FromDays(60)))
                    .RuleFor(x => x.LastChangeDate,
                        f => f.Date.Between(DateTime.Now - TimeSpan.FromDays(60), DateTime.Now))
                    .RuleFor(x => x.UserCreateDate, f => dbUserGenerator.Generate())
                    .RuleFor(x => x.UserLastChangeDate, f => dbUserGenerator.Generate())

                    .RuleFor(x => x.UsersPromocodes, f => null)
                    .RuleFor(x => x.FavoritesUsersId, f => RandomFromListUserId(listUserId, 3))
                    .RuleFor(x => x.RatingUsersId, f => RandomFromListUserId(listUserId, 5))

                    .RuleFor(x => x.Translations, f => translationGenerator.Generate(1))

                    .RuleFor(x => x.PromocodeOptions, f => promocodeOptionGenerator.Generate())
                ;

            for (var i = 0; i < count; i++)
            {
                yield return discountGenerator.Generate();
            }
        }

        private static IEnumerable<string> RandomFromListUserId(IReadOnlyList<string> values, int n)
        {
            var countValues = values.Count;
            var random = new Random();

            var results = new List<string>();

            for (var i = 0; i < n; i++)
            {
                var value = random.Next(0, countValues);
                results.Add(values[value]);
            }

            return results.Distinct().ToList();
        }
    }
}
