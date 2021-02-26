using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models.Promocode;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Extentions
{
    public class DiscountExtentionsTests
    {
        [Fact]
        public void TranslateTest()
        {
            var discount = new Discount
            {
                Id = Guid.Parse("4da5c6e4-1cf4-4bb4-b922-b43a3037ca62"),
                Address = new Address
                {
                    Location = new Location
                    {
                        Longitude = 0,
                        Latitude = 0
                    }
                },
                Company = new Company(),
                Translations = new List<Translation>
                {
                    new Translation
                    {
                        Name = "Name",
                        Description = "Description",
                        Address = new TranslationAddress(),
                        Company = new TranslationCompany(),
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
            var value = new Discount
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
            var value = new Discount
            {
                Id = Guid.Parse("e8574d77-8c3c-4e5c-aa7c-d7a84f7f30c7")
            }.ToUpsertDiscountRequest();
            value.Should().NotBeNull();
        }

        [Fact]
        public void ToPromocodeNullTest()
        {
            var value = ((DbPromocode)null).ToPromocode();
            value.Should().BeNull();
        }

        [Fact]
        public void ToPromocodeNullEmptyTest()
        {
            var value = new DbPromocode();
            value.ToPromocode().Should().BeNull();
        }

        [Fact]
        public void ToPromocodeNotNullTest()
        {
            var value = new DbPromocode
            {
                Id = "dc666524-36a3-43ef-998c-7f250793d9bc",
                CreateDate = "01.01.2020 10:10:10".GetUtcDateTime(),
                EndDate = "01.01.2021 10:10:10".GetUtcDateTime(),
                Deleted = false,
                PromocodeValue = StringExtentions.NewPromocodeValue()
            }.ToPromocode();
            value.Should().NotBeNull();
            value.PromocodeValue.Should().NotBeEmpty();
        }

        [Fact]
        public void ToDbPromocodeNullTest()
        {
            var value = ((Promocode)null).ToDbPromocode();
            value.Should().BeNull();
        }

        [Fact]
        public void ToDbPromocodeNullEmptyTest()
        {
            var value = new Promocode();
            value.ToDbPromocode().Should().BeNull();
        }

        [Fact]
        public void ToDbPromocodeNotNullTest()
        {
            var value = new Promocode
            {
                Id = Guid.Parse("dc666524-36a3-43ef-998c-7f250793d9bc"),
                CreateDate = "01.01.2020 10:10:10".GetUtcDateTime(),
                EndDate = "01.01.2021 10:10:10".GetUtcDateTime(),
                Deleted = false,
                PromocodeValue = StringExtentions.NewPromocodeValue()
            }.ToDbPromocode();
            value.Should().NotBeNull();
        }

    }
}
