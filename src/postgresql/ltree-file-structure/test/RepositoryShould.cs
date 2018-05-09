using System;
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

        [Fact]
        public void ReturnRootNodes()
        {
            // Act
            var actual = repository.GetRootNodes(connection);

            actual.Length.ShouldBe(10000);
        }
    }
}
