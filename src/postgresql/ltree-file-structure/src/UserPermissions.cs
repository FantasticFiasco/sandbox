using System;
using Dapper;
using Npgsql;

namespace FileSystem
{
    public class UserPermissions
    {
        private NpgsqlConnection connection;

        public UserPermissions(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public void Add(string userId, Node node, Role role)
        {
            connection.Execute(
                @"INSERT INTO permission
                (user_id, node_id, role_id)
                VALUES
                (@UserId, @NodeId, @RoleId)",
                new
                {
                    UserId = userId,
                    NodeId = node.Id,
                    RoleId = role.Id
                });
        }

        public UserPermission[] GetForNode(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
