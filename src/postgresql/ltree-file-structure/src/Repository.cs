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

        public Node GetFirstNodeOnLevel(NpgsqlConnection connection, int level)
        {
            return connection
                .Query<Node>(
                    @"SELECT id, name, path::TEXT
                    FROM node
                    WHERE nlevel(path) = @Level
                    LIMIT 1",
                    new
                    {
                        level = level
                    })
                .First();
        }

        public IEnumerable<Node> GetDescendants(NpgsqlConnection connection, Node parent)
        {
            return connection.Query<Node>(
                $@"SELECT id, name, path::TEXT
                FROM node
                WHERE path ~ '*.{parent.Path}.*'
                AND id != @Id",
                new
                {
                    Id = parent.Id
                });
        }

        public IEnumerable<Node> GetAncestors(NpgsqlConnection connection, Node child)
        {
            return connection.Query<Node>(
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
                    Id = child.Id
                });
        }
    }
}
