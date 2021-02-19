using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Data.Models;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Data.Models
{
    public class ModelsTests
    {
        [Fact]
        public void DbDiscountTest()
        {
            var dbDiscount = new DbDiscount()
            {
                RatingUsersId = new List<string>()
            };
            dbDiscount.RatingUsersId.Should().NotBeNull();
        }

        [Fact]
        public void DbTagTest()
        {
            var value = new DbTag()
            {
                Language = "russian",
                Name = "Name",
                Translations = new List<DbTag>()
            };
            value.Language.Should().NotBeNull();
            value.Name.Should().NotBeNull();
            value.Translations.Should().NotBeNull();
        }
    }
}
