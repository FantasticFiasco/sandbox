using System;
using System.Collections.Generic;
using FileSystem;
using Npgsql;
using NpgsqlTypes;
using Xunit;

namespace Test
{
    public static class Db
    {
        private static string ConnectionString = "Host=localhost;Username=root;Password=password";

        public static NpgsqlConnection OpenConnection()
        {
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }

        public static void EnableExtension(NpgsqlConnection connection)
        {
            Console.WriteLine("Enable LTREE extension...");

            using (var command = connection.CreateCommand())
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

        public static void RecreateTable(NpgsqlConnection connection)
        {
            Console.WriteLine("Recreate table...");

            using (var command = connection.CreateCommand())
            {
                // Table
                command.CommandText = "DROP TABLE IF EXISTS node";
                command.ExecuteNonQuery();

                command.CommandText =
                    @"CREATE TABLE node
                    (
                        id text UNIQUE NOT NULL,
                        name text NOT NULL,
                        path ltree
                    )";
                command.ExecuteNonQuery();

                // Index
                command.CommandText = "DROP INDEX IF EXISTS node_path_idx";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE INDEX node_path_idx ON node USING gist (path)";
                command.ExecuteNonQuery();
            }
        }

        public static void PopulateTable(NpgsqlConnection connection)
        {
            Console.WriteLine("Create nodes...");

            var nodes = new List<Node>();

            // Populate with:
            // - 10.000 root nodes
            // - Each node has 5 children
            // - That goes on until we have four levels
            // It would mean that we should create
            //   10.000 * (1 + 5 * 5² + 5³) = 1.560.000 nodes
            for (int levelOneIndex = 0; levelOneIndex < 10000; levelOneIndex++)
            {
                var levelOneId = NewId();
                var levelOneName = $"{levelOneIndex}";
                var levelOnePath = $"{levelOneId}";

                nodes.Add(new Node { Id = levelOneId, Name = levelOneName, Path = levelOnePath });

                for (int levelTwoIndex = 0; levelTwoIndex < 5; levelTwoIndex++)
                {
                    var levelTwoId = NewId();
                    var levelTwoName = $"{levelOneName}.{levelTwoIndex}";
                    var levelTwoPath = $"{levelOnePath}.{levelTwoId}";

                    nodes.Add(new Node { Id = levelTwoId, Name = levelTwoName, Path = levelTwoPath });

                    for (int levelThreeIndex = 0; levelThreeIndex < 5; levelThreeIndex++)
                    {
                        var levelThreeId = NewId();
                        var levelThreeName = $"{levelTwoName}.{levelThreeIndex}";
                        var levelThreePath = $"{levelTwoPath}.{levelThreeId}";

                        nodes.Add(new Node { Id = levelThreeId, Name = levelThreeName, Path = levelThreePath });

                        for (int levelFourIndex = 0; levelFourIndex < 5; levelFourIndex++)
                        {
                            var levelFourId = NewId();
                            var levelFourName = $"{levelThreeName}.{levelFourIndex}";
                            var levelFourPath = $"{levelThreePath}.{levelFourId}";

                            nodes.Add(new Node { Id = levelFourId, Name = levelFourName, Path = levelFourPath });
                        }
                    }
                }
            }

            Console.WriteLine("Write nodes...");

            using (var writer = connection.BeginTextImport("COPY node (id, name, path) FROM STDIN"))
            {
                foreach (var node in nodes)
                {
                    writer.Write($"{node.Id}\t{node.Name}\t{node.Path}\n");
                }
            }
        }

        private static string NewId() => Guid.NewGuid().ToString().Replace("-", "");
    }
}
