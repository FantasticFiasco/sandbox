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
    }
}
