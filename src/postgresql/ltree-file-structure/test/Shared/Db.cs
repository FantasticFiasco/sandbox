using System;
using Npgsql;

namespace Shared
{
    public class Db : IDisposable
    {
        private static readonly string ConnectionString = "Host=localhost;Username=root;Password=password;Max Auto Prepare=10";

        public Db()
        {
            Connection = new NpgsqlConnection(ConnectionString);
            Connection.Open();
        }

        public static string NewId() => Guid.NewGuid().ToString().Replace("-", "");

        public NpgsqlConnection Connection { get; }

        public void SetupTables()
        {
            RecreateSchema();
            EnableExtension();
            CreateTables();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }

        private void RecreateSchema()
        {
            using (var command = Connection.CreateCommand())
            {
                command.CommandText = "DROP SCHEMA IF EXISTS public CASCADE";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE SCHEMA public";
                command.ExecuteNonQuery();
            }
        }

        private void EnableExtension()
        {
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

        private void CreateTables()
        {
            using (var command = Connection.CreateCommand())
            {
                CreateNodes(command);
                CreateRoles(command);
                CreateOperations(command);
                CreatePermissions(command);
            }
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

        private static void CreateRoles(NpgsqlCommand command)
        {
            command.CommandText =
                @"CREATE TABLE role
                (
                    id text PRIMARY KEY,
                    name text NOT NULL
                )";
            command.ExecuteNonQuery();

            // Write roles
            command.CommandText = "INSERT INTO role (id, name) VALUES ('administrator', 'Administrator')";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO role (id, name) VALUES ('operator', 'Operator')";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO role (id, name) VALUES ('viewer', 'Viewer')";
            command.ExecuteNonQuery();
        }

        private static void CreateOperations(NpgsqlCommand command)
        {
            command.CommandText =
                @"CREATE TABLE operation
                (
                    id text NOT NULL,
                    name text NOT NULL,
                    role_id text REFERENCES role(id) ON DELETE CASCADE,
                    PRIMARY KEY (id, role_id)
                )";
            command.ExecuteNonQuery();

            // Write operations
            command.CommandText = "INSERT INTO operation (id, name, role_id) VALUES ('read', 'Read', 'administrator')";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO operation (id, name, role_id) VALUES ('write', 'Write', 'administrator')";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO operation (id, name, role_id) VALUES ('execute', 'Execute', 'administrator')";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO operation (id, name, role_id) VALUES ('read', 'Read', 'operator')";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO operation (id, name, role_id) VALUES ('write', 'Write', 'operator')";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO operation (id, name, role_id) VALUES ('read', 'Read', 'viewer')";
            command.ExecuteNonQuery();
        }

        private static void CreatePermissions(NpgsqlCommand command)
        {
            command.CommandText =
                @"CREATE TABLE user_permission
                (
                    user_id text NOT NULL,
                    node_id text REFERENCES node(id) ON DELETE CASCADE,
                    role_id text REFERENCES role(id) ON DELETE CASCADE
                )";
            command.ExecuteNonQuery();
        }
    }
}
