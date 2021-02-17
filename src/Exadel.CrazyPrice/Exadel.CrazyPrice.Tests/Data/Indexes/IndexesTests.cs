using Exadel.CrazyPrice.Data.Indexes;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Data.Indexes
{
    public class IndexesTests
    {
        [Fact]
        public void DbUserIndexesTest()
        {
            var indexes = DbUserIndexes.GetIndexes;
            indexes.Should().NotBeNull();
        }

        [Fact]
        public void DbTagIndexesTest()
        {
            var indexes = DbTagIndexes.GetIndexes;
            indexes.Should().NotBeNull();
        }

        [Fact]
        public void DbDiscountIndexesTest()
        {
            var indexes = DbDiscountIndexes.GetIndexes;
            indexes.Should().NotBeNull();
        }
    }
}
