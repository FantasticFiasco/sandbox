using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using FileSystem.PersistenceObjects;
using Npgsql;

namespace FileSystem
{
    public class PermissionRepository
    {
        private readonly NpgsqlConnection connection;

        public PermissionRepository(NpgsqlConnection connection)
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

        public UserPermissions[] GetForNode(Node node)
        {
            var lookup = new Dictionary<string, UserPermissions>();

            connection.Query<PermissionPo, RolePo, UserPermissions>(
                @"SELECT p.*, r.*
                FROM permission p
                JOIN role r ON r.id = p.role_id
                WHERE p.node_id = @NodeId",
                (permission, role) =>
                {
                    if (!lookup.TryGetValue(permission.user_id, out var userPermissions))
                    {
                        userPermissions = new UserPermissions
                        {
                            UserId = permission.user_id
                        };

                        lookup.Add(userPermissions.UserId, userPermissions);
                    }

                    userPermissions.Roles.Add(new Role
                    {
                        Id = role.id,
                        Name = role.name
                    });

                    return userPermissions;
                },
                new
                {
                    NodeId = node.Id
                });

            return lookup.Values.ToArray();
        }
    }
}
