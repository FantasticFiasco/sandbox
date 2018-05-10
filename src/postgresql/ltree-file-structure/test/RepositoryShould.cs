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
        private readonly Repository repository;
        private readonly NpgsqlConnection connection;

        public RepositoryShould()
        {
            repository = new Repository();
            connection = Db.OpenConnection();
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
            var actual = repository.GetNodesOnLevel(connection, level);

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
            var node = repository.GetNodesOnLevel(connection, level).First();

            // Act
            var actual = repository.GetDescendants(connection, node);

            // Assert
            actual.Count().ShouldBe(expectedCount);
        }
    }
}
