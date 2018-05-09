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
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS nodes";
                command.ExecuteNonQuery();

                command.CommandText =
                    @"CREATE TABLE nodes
                    (
                        id text UNIQUE NOT NULL,
                        name text NOT NULL,
                        path ltree
                    )";
                command.ExecuteNonQuery();
            }
        }

        public static void PopulateTable(NpgsqlConnection connection)
        {
            var nodes = new List<Node>();

            // Populate with:
            // - 10.000 root nodes
            // - Each root node has 5 children
            // - Each child have 5 grandchildren
            // - That goes on for 4 levels
            // It would mean that we should create 10.000 * 5⁴ = 6.250.000 nodes
            for (int i = 0; i < 10000; i++)
            {
                var firstLevelId = NewId();
                nodes.Add(new Node{ Id = firstLevelId, Name = $"{i}", Path = firstLevelId });

                for (int j = 0; j < 5; j++)
                {
                    var secondLevelId = NewId();
                    nodes.Add(new Node{ Id = secondLevelId, Name = $"{i}.{j}", Path = $"{firstLevelId}.{secondLevelId}" });

                    for (int k = 0; k < 5; k++)
                    {
                        var thirdLevelId = NewId();
                        nodes.Add(new Node{ Id = thirdLevelId, Name = $"{i}.{j}.{k}", Path = $"{firstLevelId}.{secondLevelId}.{thirdLevelId}" });

                        for (int l = 0; l < 5; l++)
                        {
                            var fourthLevelId = NewId();
                            nodes.Add(new Node{ Id = fourthLevelId, Name = $"{i}.{j}.{k}.{l}", Path = $"{firstLevelId}.{secondLevelId}.{thirdLevelId}.{fourthLevelId}" });

                            for (int m = 0; m < 5; m++)
                            {
                                var fifthLevelId = NewId();
                                nodes.Add(new Node{ Id = fifthLevelId, Name = $"{i}.{j}.{k}.{l}.{m}", Path = $"{firstLevelId}.{secondLevelId}.{thirdLevelId}.{fourthLevelId}.{fifthLevelId}" });
                            }
                        }
                    }
                }
            }

            using (var writer = connection.BeginTextImport("COPY nodes (id, name, path) FROM STDIN"))
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
