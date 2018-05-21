using System.Collections.Generic;
using System.Linq;
using Dapper;
using Npgsql;

namespace FileSystem
{
    public class NodeRepository
    {
        private readonly NpgsqlConnection connection;

        public NodeRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public Node[] GetNodesOnLevel(int level)
        {
            return connection
                .Query<Node>(
                    @"SELECT id, name, path::TEXT
                    FROM node
                    WHERE nlevel(path) = @Level",
                    new
                    {
                        Level = level
                    })
                .ToArray();
        }

        public Node GetFirstNodeOnLevel(int level)
        {
            return connection
                .Query<Node>(
                    @"SELECT id, name, path::TEXT
                    FROM node
                    WHERE nlevel(path) = @Level
                    LIMIT 1",
                    new
                    {
                        Level = level
                    })
                .First();
        }

        public Node[] GetDescendants(Node parent)
        {
            return connection
                .Query<Node>(
                    $@"SELECT id, name, path::TEXT
                    FROM node
                    WHERE path ~ '*.{parent.Path}.*'
                    AND id != @Id",
                    new
                    {
                        parent.Id
                    })
                .ToArray();
        }

        public Node[] GetAncestors(Node child)
        {
            return connection
                .Query<Node>(
                    $@"SELECT id, name, path::TEXT
                    FROM node
                    WHERE path @>
                        (
                            SELECT path
                            FROM node
                            WHERE id = '{child.Id}'
                        )
                    AND id != @Id",
                    new
                    {
                        child.Id
                    })
                .ToArray();
        }

        public Node[] GetNodesByUser(string userId)
        {
            var nodes = connection.Query<Node>(
                @"SELECT id, name, path::TEXT
                FROM node
                JOIN user_permission ON user_permission.node_id = id
                WHERE user_id = @UserId",
                new
                {
                    UserId = userId
                });

            List<Node> retval = new List<Node>(nodes);

            foreach (var node in nodes)
            {
                var temp = GetDescendants(node);
                retval.AddRange(temp);
            }

            return retval
                .Distinct(new NodeEqualityComparer())
                .ToArray();
        }

        private class NodeEqualityComparer : IEqualityComparer<Node>
        {
            public bool Equals(Node x, Node y) => x.Id == y.Id;

            public int GetHashCode(Node obj) => obj.Id.GetHashCode();
        }
    }
}
