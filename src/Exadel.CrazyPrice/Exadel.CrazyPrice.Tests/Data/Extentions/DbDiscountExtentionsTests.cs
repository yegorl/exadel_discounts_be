using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Data.Models.Promocode;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Data.Extentions
{
    public class DbDiscountExtentionsTests
    {
        [Fact]
        public void GetOneTest()
        {
            var dbDiscounts = new List<DbDiscount>();
            dbDiscounts.GetOne().Should().BeEquivalentTo(new DbDiscount());
        }

        [Fact]
        public void IsEmptyTest()
        {
            var dbDiscount = new DbDiscount();
            dbDiscount.IsEmpty().Should().BeTrue();
        }

        [Fact]
        public void ToDiscountFreeTest()
        {
            var dbDiscount = new DbDiscount();
            dbDiscount.ToDiscount().Should().BeEquivalentTo(new Discount());
        }

        [Fact]
        public void ToDiscountTest()
        {
            var dbDiscount = new DbDiscount()
            {
                Id = "76c6f30b-288b-4424-b031-21921e550cba",
                Language = LanguageOption.En.ToString(),
                UsersPromocodes = new List<DbUserPromocodes>()
            };
            dbDiscount.ToDiscount().Should().BeEquivalentTo(new Discount()
            {
                Id = Guid.Parse("76c6f30b-288b-4424-b031-21921e550cba"),
                Language = LanguageOption.En,
                UserCreateDate = new User(),
                UserLastChangeDate = new User(),
                UsersPromocodes = null
            });
        }

        [Fact]
        public void ToDiscountInvalidGuidTest()
        {
            var dbDiscount = new DbDiscount()
            {
                Id = "invalid",
                Language = LanguageOption.En.ToString()
            };
            dbDiscount.ToDiscount().Should().BeEquivalentTo(new Discount()
            {
                Id = Guid.Empty,
                Language = LanguageOption.Ru,
            });
        }

        [Fact]
        public void ToDbDiscountTest()
        {
            var discount = new Discount()
            {
                Id = Guid.Parse("76c6f30b-288b-4424-b031-21921e550cba"),
                Language = LanguageOption.Ru,
                Company = new Company(),
                Address = new Address(),
                Tags = new List<string>()
            };
            discount.ToDbDiscount().Should().BeEquivalentTo(new DbDiscount()
            {
                Id = "76c6f30b-288b-4424-b031-21921e550cba",
                Language = "russian",
                Company = null,
                Address = null,
                Tags = new List<string>()
            });
        }

        [Fact]
        public void ToLocationTest()
        {
            var value = new DbLocation();
            value.ToLocation().Should().BeNull();
        }

        [Fact]
        public void ToDbLocationTest()
        {
            var value = new Location();
            value.ToDbLocation().Should().BeNull();
        }

        [Fact]
        public void ToTranslationsTest()
        {
            var value = new List<DbTranslation>()
            {
                new()
                {
                    Name = "Name",
                    Language = "russian"
                }
            };
            value.ToTranslations().First().Name.Should().BeEquivalentTo("Name");
        }

        [Fact]
        public void ToDbTranslationsTest()
        {
            var value = new List<Translation>()
            {
                new()
                {
                    Name = "Name",
                    Language = LanguageOption.Ru
                }
            };
            value.ToDbTranslations().First().Name.Should().BeEquivalentTo("Name");
        }

        [Fact]
        public void IntNullTest()
        {
            int? value = null;
            (5 > value).Should().BeFalse();
        }
    }
}
