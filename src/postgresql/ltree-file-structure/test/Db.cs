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

        public Db()
        {
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
            Console.WriteLine("Recreate table 'node'...");

            using (var command = Connection.CreateCommand())
            {
                DropPermissions(command);
                DropOperations(command);
                DropRoles(command);
                DropNodes(command);

                CreateNodes(command);
                CreateRoles(command);
                CreateOperations(command);
                CreatePermissions(command);
            }
        }

        private static void DropNodes(NpgsqlCommand command)
        {
            command.CommandText = "DROP INDEX IF EXISTS node_path_idx";
            command.ExecuteNonQuery();

            command.CommandText = "DROP TABLE IF EXISTS node";
            command.ExecuteNonQuery();
        }

        private static void CreateNodes(NpgsqlCommand command)
        {
            command.CommandText =
                @"CREATE TABLE node
                (
                    id text PRIMARY KEY,
                    name text NOT NULL,
                    path ltree
                )";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE INDEX node_path_idx ON node USING gist (path)";
            command.ExecuteNonQuery();
        }

        private static void DropRoles(NpgsqlCommand command)
        {
            command.CommandText = "DROP TABLE IF EXISTS role";
            command.ExecuteNonQuery();
        }

        private static void CreateRoles(NpgsqlCommand command)
        {
            command.CommandText =
                @"CREATE TABLE role
                (
                    id text PRIMARY KEY,
                    name text NOT NULL
                )";
            command.ExecuteNonQuery();
        }

        private static void DropOperations(NpgsqlCommand command)
        {
            command.CommandText = "DROP TABLE IF EXISTS operation";
            command.ExecuteNonQuery();
        }

        private static void CreateOperations(NpgsqlCommand command)
        {
            command.CommandText =
                @"CREATE TABLE operation
                (
                    id text PRIMARY KEY,
                    name text NOT NULL,
                    role_id text REFERENCES role(id) ON DELETE CASCADE
                )";
            command.ExecuteNonQuery();
        }

        private static void DropPermissions(NpgsqlCommand command)
        {
            command.CommandText = "DROP TABLE IF EXISTS permission";
            command.ExecuteNonQuery();
        }

        private static void CreatePermissions(NpgsqlCommand command)
        {
            command.CommandText =
                @"CREATE TABLE permission
                (
                    user_id text PRIMARY KEY,
                    node_id text REFERENCES node(id) ON DELETE CASCADE,
                    role_id text REFERENCES role(id) ON DELETE CASCADE
                )";
            command.ExecuteNonQuery();
        }
    }
}
