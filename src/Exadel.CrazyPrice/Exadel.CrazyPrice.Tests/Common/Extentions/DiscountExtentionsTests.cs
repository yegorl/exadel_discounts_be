using System;
using System.Collections.Generic;
using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Extentions
{
    public class DiscountExtentionsTests
    {
        [Fact]
        public void TranslateTest()
        {
            var discount = new Discount()
            {
                Id = Guid.Parse("4da5c6e4-1cf4-4bb4-b922-b43a3037ca62"),
                Address = new Address()
                {
                    Location = new Location()
                    {
                        Longitude = 0,
                        Latitude = 0
                    }
                },
                Company = new Company(),
                Translations = new List<Translation>()
                {
                    new Translation()
                    {
                        Name = "Name",
                        Description = "Description",
                        Address = new Address(),
                        Company = new Company(),
                        Tags = new List<string>(),
                        Language = LanguageOption.En
                        
                    }
                }
            };

            var translateDiscount = discount.Translate(LanguageOption.En);
            translateDiscount.Name.Should().BeEquivalentTo("Name");
        }

        [Fact]
        public void IsEmptyDiscountTrueTest()
        {
            var value = new Discount();
            value.IsEmpty().Should().BeTrue();
        }

        [Fact]
        public void IsEmptyDiscountFalseTest()
        {
            var value = new Discount()
            {
                Id = Guid.Parse("e8574d77-8c3c-4e5c-aa7c-d7a84f7f30c7")
            };

            value.IsEmpty().Should().BeFalse();
        }

        [Fact]
        public void ToUpsertDiscountRequestNullTest()
        {
            var value = new Discount().ToUpsertDiscountRequest();
            value.Should().BeNull();
        }

        [Fact]
        public void ToUpsertDiscountRequestNotNullTest()
        {
            var value = new Discount()
            {
                Id = Guid.Parse("e8574d77-8c3c-4e5c-aa7c-d7a84f7f30c7")
            }.ToUpsertDiscountRequest();
            value.Should().NotBeNull();
        }
    }
}
