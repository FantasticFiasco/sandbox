using System;
using System.Linq;
using FileSystem;
using Npgsql;
using Shouldly;
using Xunit;

namespace Test
{
    public class RepositoryShould : IDisposable
    {
        private readonly NpgsqlConnection connection;
        private readonly Repository repository;

        public RepositoryShould()
        {
            connection = Db.OpenConnection();
            repository = new Repository(connection);
        }

        public void Dispose()
        {
            connection?.Dispose();
        }

        [Theory]
        [InlineData(1, 10000)]
        [InlineData(2, 10000 * 5)]
        [InlineData(3, 10000 * 5 * 5)]
        [InlineData(4, 10000 * 5 * 5 * 5)]
        [InlineData(5, 0)]
        public void ReturnNodesGivenLevel(int level, int expectedCount)
        {
            // Act
            var actual = repository.GetNodesOnLevel(level);

            // Assert
            actual.Count().ShouldBe(expectedCount);
        }

        [Theory]
        [InlineData(1, 5 + (5 * 5) + (5 * 5 * 5))]
        [InlineData(2, 5 + (5 * 5))]
        [InlineData(3, 5)]
        [InlineData(4, 0)]
        public void ReturnDescendantsGivenLevel(int level, int expectedCount)
        {
            // Arrange
            var node = repository.GetFirstNodeOnLevel(level);

            // Act
            var actual = repository.GetDescendants(node);

            // Assert
            actual.Count().ShouldBe(expectedCount);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        public void ReturnAncestorsGivenLevel(int level, int expectedCount)
        {
            // Arrange
            var node = repository.GetFirstNodeOnLevel(level);

            // Act
            var actual = repository.GetAncestors(node);

            // Assert
            actual.Count().ShouldBe(expectedCount);
        }
    }
}
