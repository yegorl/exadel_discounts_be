using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Exadel.CrazyPrice.Data.Models;

namespace Exadel.CrazyPrice.Data.Seeder
{
    public class FakeDiscounts
    {
        public IEnumerable<DbDiscount> Get(uint count)
        {
            var local = "ru";
            var languages = new Dictionary<string, string>
            {
                ["ru"] = "russian",
                ["en"] = "english"
            };

            var locationGenerator = new Faker<DbLocation>(local)
                    .RuleFor(x => x.Latitude, f => f.Address.Latitude())
                    .RuleFor(x => x.Longitude, f => f.Address.Longitude())
                ;

            var companyGenerator = new Faker<DbCompany>(local)
                    .RuleFor(x => x.Name, f => f.Company.CompanyName())
                    .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+!!! !! !!!-!!-!!"))
                    .RuleFor(x => x.Mail, f => f.Person.Email)
                ;

            var addressGenerator = new Faker<DbAddress>(local)
                    .StrictMode(true)
                    .RuleFor(x => x.Country, f => f.Address.Country())
                    .RuleFor(x => x.City, f => f.Address.City())
                    .RuleFor(x => x.Street, f => f.Address.StreetAddress())
                    .RuleFor(x => x.Location, f => locationGenerator.Generate())
                ;

           var dbUserGenerator = new Faker<DbUser>(local)
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

            var addressTranslationGenerator = new Faker<DbAddress>("en")
                    .StrictMode(true)
                    .RuleFor(x => x.Country, f => f.Address.Country())
                    .RuleFor(x => x.City, f => f.Address.City())
                    .RuleFor(x => x.Street, f => f.Address.StreetAddress())
                    .RuleFor(x => x.Location, f => null)
                ;

            var translationGenerator = new Faker<DbTranslation>("en")
                    .RuleFor(x => x.Language, f => languages["en"]) // !!!!
                    .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                    .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(x => x.Address, f => addressTranslationGenerator.Generate())
                    .RuleFor(x => x.Company, f => companyTranslationGenerator.Generate())
                    .RuleFor(x => x.Tags, f => f.Commerce.Categories(15).Distinct().ToList())
                ;

            var discountGenerator = new Faker<DbDiscount>(local)
                    .StrictMode(true)
                    .RuleFor(x => x.Id, f => Guid.NewGuid().ToString())
                    .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                    .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(x => x.AmountOfDiscount, f => f.Random.Int(500, 7000) / 100)
                    .RuleFor(x => x.StartDate, f => f.Date.Between(DateTime.Now - TimeSpan.FromDays(700), DateTime.Now))
                    .RuleFor(x => x.EndDate,
                        f => f.Date.Between(DateTime.Now + TimeSpan.FromDays(1), DateTime.Now + TimeSpan.FromDays(700)))
                    .RuleFor(x => x.Address, f => addressGenerator.Generate())
                    .RuleFor(x => x.Company, f => companyGenerator.Generate())
                    .RuleFor(x => x.WorkingDaysOfTheWeek,
                        f => string.Join("", f.Random.Int(0, 1), f.Random.Int(0, 1), f.Random.Int(0, 1),
                            f.Random.Int(0, 1), f.Random.Int(0, 1), f.Random.Int(0, 1), f.Random.Int(0, 1)))
                    .RuleFor(x => x.Tags, f => f.Commerce.Categories(15).Distinct().ToList())
                    .RuleFor(x => x.RatingTotal, f => f.Random.Int(0, 4) + f.Random.Int(1, 9) / 10)
                    .RuleFor(x => x.ViewsTotal, f => f.Random.Int(0, 100))
                    .RuleFor(x => x.SubscriptionsTotal, f => f.Random.Int(0, 50))
                    .RuleFor(x => x.Language, f => languages[local])
                    .RuleFor(x => x.Deleted, f => false)

                    .RuleFor(x => x.CreateDate,
                        f => f.Date.Between(DateTime.Now - TimeSpan.FromDays(700),
                            DateTime.Now - TimeSpan.FromDays(60)))
                    .RuleFor(x => x.LastChangeDate,
                        f => f.Date.Between(DateTime.Now - TimeSpan.FromDays(60), DateTime.Now))
                    .RuleFor(x => x.UserCreateDate, f => dbUserGenerator.Generate())
                    .RuleFor(x => x.UserLastChangeDate, f => dbUserGenerator.Generate())

                    .RuleFor(x => x.SubscriptionsUsersId, f => new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }) // !!!!
                    .RuleFor(x => x.FavoritesUsersId, f => new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }) // !!!!
                    .RuleFor(x => x.RatingUsersId, f => new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }) // !!!!

                    .RuleFor(x => x.Translations, f => translationGenerator.Generate(1)) // !!!!

                ;

            for (var i = 0; i < count; i++)
            {
                yield return discountGenerator.Generate();
            }
        }
    }
}
