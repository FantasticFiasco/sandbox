using System;
using System.Collections.Generic;
using FileSystem;
using Npgsql;
using NpgsqlTypes;
using Xunit;

namespace Test
{
    public class Db : IDisposable
    {
        private static readonly string ConnectionString = "Host=localhost;Username=root;Password=password";

        private readonly string tableName;


        public Db(string tableName)
        {
            this.tableName = tableName;

            Connection = new NpgsqlConnection(ConnectionString);
            Connection.Open();
        }

        public NpgsqlConnection Connection { get; private set; }

        public string NewId() => Guid.NewGuid().ToString().Replace("-", "");

        public void SetupTable()
        {
            EnableExtension();
            RecreateTable();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }

        private void EnableExtension()
        {
            Console.WriteLine("Enable LTREE extension...");

            using (var command = Connection.CreateCommand())
            {
                try
                {
                    command.CommandText = "CREATE EXTENSION ltree";
                    command.ExecuteNonQuery();
                }
                catch (PostgresException e) when (e.SqlState == "42710")
                {
                    // Extension is already enabled
                }
            }
        }

        private void RecreateTable()
        {
            Console.WriteLine($"Recreate table {tableName}...");

            using (var command = Connection.CreateCommand())
            {
                // Table
                command.CommandText = $"DROP TABLE IF EXISTS {tableName}";
                command.ExecuteNonQuery();

                command.CommandText =
                    $@"CREATE TABLE {tableName}
                    (
                        id text UNIQUE NOT NULL,
                        name text NOT NULL,
                        path ltree
                    )";
                command.ExecuteNonQuery();

                // Index
                command.CommandText = $"DROP INDEX IF EXISTS {tableName}_path_idx";
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE INDEX {tableName}_path_idx ON {tableName} USING gist (path)";
                command.ExecuteNonQuery();
            }
        }
    }
}
