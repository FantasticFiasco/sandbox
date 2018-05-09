using System.Collections.Generic;
using Npgsql;

namespace FileSystem
{
    public class Repository
    {
        public Node[] GetRootNodes(NpgsqlConnection connection)
        {
            var nodes = new List<Node>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT id, name, path::TEXT from nodes WHERE nlevel(path) = 1";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var node = new Node
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Path = reader.GetString(2)
                    };

                    nodes.Add(node);
                }
            }

            return nodes.ToArray();
        }
    }
}
