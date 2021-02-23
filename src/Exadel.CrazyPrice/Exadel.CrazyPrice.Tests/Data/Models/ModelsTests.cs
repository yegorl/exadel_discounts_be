using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Data.Models;
using Exadel.CrazyPrice.Data.Models.Promocode;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Data.Models
{
    public class ModelsTests
    {
        [Fact]
        public void DbDiscountTest()
        {
            var dbDiscount = new DbDiscount
            {
                RatingUsersId = new List<string>()
            };
            dbDiscount.RatingUsersId.Should().NotBeNull();
        }

        [Fact]
        public void DbTagTest()
        {
            var value = new DbTag
            {
                Language = "russian",
                Name = "Name",
                Translations = new List<DbTag>()
            };
            value.Language.Should().NotBeNull();
            value.Name.Should().NotBeNull();
            value.Translations.Should().NotBeNull();
        }

        [Fact]
        public void ToDbUsersPromocodesTest()
        {
            var usersPromocodes = new List<UserPromocodes>
            {
                new()
                {
                    UserId = Guid.Parse("adcd6b85-e7af-4041-99a0-2ba0209edf90")
                }

            };
            usersPromocodes.ToDbUsersPromocodes().Should().NotBeNull();
        }

        [Fact]
        public void ToUsersPromocodesTest()
        {
            var usersPromocodes = new List<DbUserPromocodes>
            {
                new()
                {
                    UserId = "adcd6b85-e7af-4041-99a0-2ba0209edf90",
                }

            };
            usersPromocodes.ToUsersPromocodes().Should().NotBeNull();
        }

        [Fact]
        public void ToDbUsersPromocodesListNullTest()
        {
            var usersPromocodes = new UserPromocodes
            {
                UserId = Guid.Parse("adcd6b85-e7af-4041-99a0-2ba0209edf90")
            };
            usersPromocodes.ToDbUserPromocodes().Should().BeNull();
        }

        [Fact]
        public void ToDbUsersPromocodesListOkTest()
        {
            var usersPromocodes = new UserPromocodes
            {
                UserId = Guid.Parse("adcd6b85-e7af-4041-99a0-2ba0209edf90"),
                Promocodes = new List<Promocode>
                {
                    new()
                    {
                        Id = Guid.Parse("f5219e35-14f7-4e7e-8c7c-232e464b9ced")
                    }
                }
            };
            usersPromocodes.ToDbUserPromocodes().Should().NotBeNull();
        }

        [Fact]
        public void ToUsersPromocodesListNullTest()
        {
            var usersPromocodes = new DbUserPromocodes
            {
                UserId = "adcd6b85-e7af-4041-99a0-2ba0209edf90"
            };
            usersPromocodes.ToUserPromocodes().Should().BeNull();
        }

        [Fact]
        public void ToUsersPromocodesListOkTest()
        {
            var usersPromocodes = new DbUserPromocodes
            {
                UserId = "adcd6b85-e7af-4041-99a0-2ba0209edf90",
                Promocodes = new List<DbPromocode>
                {
                    new()
                    {
                        Id = "20469015-0023-4771-9726-cea30e4740ac"
                    }
                }
            };
            usersPromocodes.ToUserPromocodes().Should().NotBeNull();
        }

        [Fact]
        public void DbPromocodeTest()
        {
            var usersPromocodes = DbPromocode.New(new Guid(), DateTime.Now, 1, StringExtentions.NewPromocodeValue());
            usersPromocodes.Should().NotBeNull();
        }

        [Theory]
        [InlineData(100,2, false, false)]
        [InlineData(1,1, false, false)]
        [InlineData(20,2, false, false)]
        [InlineData(15,1, false, false)]
        [InlineData(15,0, false, false)]
        [InlineData(20,3, false, true)]
        [InlineData(20,5, true, true)]
        [InlineData(10,5, true, true)]
        [InlineData(null,null, false, true)]
        public void ToUsersPromocodesCanAddTest(
            int? time,
            int? count,
            bool deleted,
            bool expectedValue
            )
        {
            var usersPromocodes = new DbUserPromocodes
            {
                UserId = "adcd6b85-e7af-4041-99a0-2ba0209edf90",
                Promocodes = new List<DbPromocode>
                {
                    new()
                    {
                        Id = "20469015-0023-4771-9726-cea30e4740ac",
                        CreateDate = "10.10.2021 10:10:10".GetUtcDateTime(),
                        EndDate =    "10.10.2021 10:10:20".GetUtcDateTime(),
                        Deleted = deleted,
                        PromocodeValue = "LKJ"
                    }
                    ,
                    new()
                    {
                        Id = "1d168959-0966-4e48-a67a-61131ab5debc",
                        CreateDate = "10.10.2021 10:10:10".GetUtcDateTime(),
                        EndDate =    "10.10.2021 10:10:40".GetUtcDateTime(),
                        Deleted = deleted,
                        PromocodeValue = "LKJ"
                    },
                    new()
                    {
                        Id = "f5b4dd52-0224-4838-8022-75acc8f6dfd7",
                        CreateDate = "10.10.2021 10:10:10".GetUtcDateTime(),
                        EndDate =    "10.10.2021 10:12:40".GetUtcDateTime(),
                        Deleted = true,
                        PromocodeValue = "LKJ"
                    },
                    new()
                    {
                        Id = "8e414090-8243-4dd4-8825-1aab5386203c",
                        CreateDate = "10.10.2021 10:10:10".GetUtcDateTime(),
                        EndDate =    "10.10.2021 12:10:40".GetUtcDateTime(),
                        Deleted = false,
                        PromocodeValue = "LKJ"
                    }
                }
            };
            var timeNow = "10.10.2021 10:10:35".GetUtcDateTime();
            usersPromocodes.CanAdd(time, count, timeNow).Should().Be(expectedValue);
        }

        [Fact]
        public void ToUsersPromocodesCanAddFalseTest()
        {
            var usersPromocodes = new DbUserPromocodes
            {
                UserId = "adcd6b85-e7af-4041-99a0-2ba0209edf90",
                Promocodes = new List<DbPromocode>
                {
                    new()
                    {
                        Id = "20469015-0023-4771-9726-cea30e4740ac",
                        CreateDate = "10.10.2020 10:10:10".GetUtcDateTime(),
                        EndDate = "10.10.2021 10:10:10".GetUtcDateTime(),
                        Deleted = false,
                        PromocodeValue = "LKJ"
                    }
                }
            };
            usersPromocodes.CanAdd(1, 2, DateTime.UtcNow).Should().BeTrue();
        }
    }
}
