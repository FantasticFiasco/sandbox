using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using System;

namespace Sample
{
    public static class Db
    {
        private static readonly string TableName = "ltree_sample";

        public static void Seed()
        {
            using (var connection = OpenConnection())
            {
                EnableExtension(connection);
                CleanTable(connection);
                SeedTable(connection);
            }
        }

        public static NpgsqlConnection OpenConnection()
        {
            var connection = new NpgsqlConnection(ConnectionStringBuilder.ToString());
            connection.Open();

            return connection;
        }

        private static void EnableExtension(NpgsqlConnection connection)
        {
            try
            {
                Console.WriteLine("Enable LTREE extension...");
                connection.Execute("create extension ltree");
            }
            catch (PostgresException)
            {
                Console.WriteLine("LTREE extensions is already enabled");
            }
        }

        private static void CleanTable(NpgsqlConnection connection)
        {
                Console.WriteLine($"Drop table '{TableName}'...");
                connection.Execute($"DROP TABLE IF EXISTS {TableName}");

                Console.WriteLine($"Recreate table '{TableName}'...");
                connection.Execute(
                    $@"CREATE TABLE {TableName}
                    (
                        id serial primary key,
                        letter char,
                        path ltree
                    )");

                Console.WriteLine($"Create index on table '{TableName}'...");
                connection.Execute($"CREATE INDEX tree_path_idx ON {TableName} using gist (path);");
        }

        private static void SeedTable(NpgsqlConnection connection)
        {
            Console.WriteLine($"Seed table '{TableName}'...");

            connection.Execute($"INSERT INTO {TableName} (letter, path) values ('A', 'A')");
            connection.Execute($"INSERT INTO {TableName} (letter, path) values ('B', 'A.B')");
            connection.Execute($"INSERT INTO {TableName} (letter, path) values ('C', 'A.C')");
            connection.Execute($"INSERT INTO {TableName} (letter, path) values ('D', 'A.C.D')");
            connection.Execute($"INSERT INTO {TableName} (letter, path) values ('E', 'A.C.E')");
            connection.Execute($"INSERT INTO {TableName} (letter, path) values ('F', 'A.C.F')");
            connection.Execute($"INSERT INTO {TableName} (letter, path) values ('G', 'A.B.G')");
        }

        private static NpgsqlConnectionStringBuilder ConnectionStringBuilder
        {
             get
             {
                var configuration = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();

                return new NpgsqlConnectionStringBuilder
                {
                    Host = configuration.GetSection("DB_HOST").Value,
                    Username = configuration.GetSection("DB_USERNAME").Value,
                    Password = configuration.GetSection("DB_PASSWORD").Value
                };
             }
        }
    }
}
