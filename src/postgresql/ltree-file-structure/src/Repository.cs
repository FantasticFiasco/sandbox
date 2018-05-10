using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Npgsql;

namespace FileSystem
{
    public class Repository
    {
        public IEnumerable<Node> GetNodesOnLevel(NpgsqlConnection connection, int level)
        {
            return connection.Query<Node>(
                @"SELECT id, name, path::TEXT
                FROM node
                WHERE nlevel(path) = @Level",
                new
                {
                    level = level
                });
        }

        public IEnumerable<Node> GetDescendants(NpgsqlConnection connection, Node parent)
        {
            return connection
                .Query<Node>(
                    $@"SELECT id, name, path::TEXT
                    FROM node
                    WHERE '{parent.Path}' @> path")
                .Where(node => node.Id != parent.Id);
        }
    }
}
