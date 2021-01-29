using Bogus;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Xunit;
using Person = Exadel.CrazyPrice.Common.Models.Person;

namespace Exadel.CrazyPrice.Tests
{
    public class DiscountFakeTests
    {
        [Fact]
        public void GetDiscountFakeTest()
        {
            var local = "en";

            var workingHours = string.Empty;
            workingHours = local == "ru"
                ? "понедельник 09:00–18:00 вторник 09:00–18:00 среда 09:00–18:00 четверг 09:00–18:00 пятница 09:00–18:00 суббота Закрыто воскресенье Закрыто"
                : "Monday 09:00–18:00 Tuesday 09:00–18:00 Wednesday 09:00–18:00 Thursday 09:00–18:00 Friday 09:00–18:00 Saturday Closed Sunday Closed";

            var personGenerator = new Faker<Person>(local)
                    .RuleFor(x => x.Id, f => Guid.NewGuid())
                    .RuleFor(x => x.Name, f => f.Person.FirstName)
                    .RuleFor(x => x.Surname, f => f.Person.LastName)
                    .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+!!! !! !!!-!!-!!"))
                    .RuleFor(x => x.Mail, f => f.Person.Email)
                ;

            var companyGenerator = new Faker<Company>(local)
                    .RuleFor(x => x.Name, f => f.Company.CompanyName())
                    .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber("+!!! !! !!!-!!-!!"))
                ;

            var locationGenerator = new Faker<Location>(local)
                    .RuleFor(x => x.Latitude, f => f.Address.Latitude())
                    .RuleFor(x => x.Longitude, f => f.Address.Longitude())
                ;


            var addressGenerator = new Faker<Address>(local)
                    .StrictMode(true)
                    .RuleFor(x => x.Country, f => f.Address.Country())
                    .RuleFor(x => x.City, f => f.Address.City())
                    .RuleFor(x => x.Street, f => f.Address.StreetAddress())
                    .RuleFor(x => x.Location, f => locationGenerator.Generate())
                ;


            var discountDataGenerator = new Faker<DiscountResponse>(local)
                .StrictMode(true)
                .RuleFor(x => x.Id, f => Guid.NewGuid())
                .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                .RuleFor(x => x.AmountOfDiscount, f => f.Random.Int(5000, 10000)/100)
                .RuleFor(x => x.StartDate, f => f.Date.Between(DateTime.Now - TimeSpan.FromDays(700), DateTime.Now))
                .RuleFor(x => x.EndDate, f => f.Date.Between(DateTime.Now + TimeSpan.FromDays(1), DateTime.Now + TimeSpan.FromDays(700)))
                .RuleFor(x => x.Address, f => addressGenerator.Generate())
                .RuleFor(x => x.Company, f => companyGenerator.Generate())
                .RuleFor(x => x.WorkingHours, f => workingHours)
                .RuleFor(x => x.Tags, f => f.Commerce.Categories(7).Distinct().ToList())
                .RuleFor(x => x.RatingTotal, f => f.Random.Int(0, 4) + f.Random.Int(1, 9) / 10)
                .RuleFor(x => x.ViewTotal, f => f.Random.Int(0, 100))
                .RuleFor(x => x.ViewPersonsId, f => new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }) // !!!!
                .RuleFor(x => x.ReservationTotal, f => f.Random.Int(0, 50))
                .RuleFor(x => x.ReservationPersonsId, f => new List<string> { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }) // !!!!
                .RuleFor(x => x.CreateDate, f => f.Date.Between(DateTime.Now - TimeSpan.FromDays(700), DateTime.Now - TimeSpan.FromDays(60)))
                .RuleFor(x => x.LastChangeDate, f => f.Date.Between(DateTime.Now - TimeSpan.FromDays(60), DateTime.Now))
                .RuleFor(x => x.PersonCreateDate, f => personGenerator.Generate())
                .RuleFor(x => x.PersonLastChangeDate, f => personGenerator.Generate())
                ;


            var discountData = new List<DiscountResponse>();

            for (var i = 0; i < 12; i++)
            {
                discountData.Add(discountDataGenerator.Generate());
            }

            var discountDataJson = JsonConvert.SerializeObject(discountData, formatting: Formatting.Indented);
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "discountData.json");
            File.WriteAllText(path, discountDataJson);
        }
    }
}
