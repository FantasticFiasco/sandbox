using System.Collections.Generic;
using System.Linq;
using Dapper;
using FileSystem.PersistenceObjects;
using Npgsql;

namespace FileSystem
{
    public class UserPermissionsRepository
    {
        private readonly NpgsqlConnection connection;

        public UserPermissionsRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public void Add(string userId, Node node, Role role)
        {
            connection.Execute(
                @"INSERT INTO user_permission
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
            var userPermissionsLookup = new Dictionary<string, UserPermissions>();
            var roleLookup = new Dictionary<string, Role>();

            connection.Query<PermissionPo, RolePo, OperationPo, UserPermissions>(
                $@"SELECT user_permission.*, role.*, operation.*
                FROM user_permission
                JOIN role ON role.id = user_permission.role_id
                JOIN operation ON operation.role_id = role.id
                WHERE user_permission.node_id = ANY(
                    SELECT id
                    FROM node
                    WHERE path @>
                        (
                            SELECT path
                            FROM node
                            WHERE id = '{node.Id}'
                        )
                )",
                (userPermissionPo, rolePo, operationPo) =>
                {
                    if (!userPermissionsLookup.TryGetValue(userPermissionPo.user_id, out var userPermissions))
                    {
                        userPermissions = new UserPermissions(userPermissionPo.user_id);

                        userPermissionsLookup.Add(userPermissions.UserId, userPermissions);
                    }

                    if (!roleLookup.TryGetValue(rolePo.id, out var role))
                    {
                        var inheritedFrom = userPermissionPo.node_id == node.Id
                            ? null
                            : new Reference(userPermissionPo.node_id);

                        role = new Role(rolePo.id, rolePo.name, inheritedFrom);
                        userPermissions.Roles.Add(role);

                        roleLookup.Add(role.Id, role);
                    }

                    role.Operations.Add(new Operation
                    {
                        Id = operationPo.id,
                        Name = operationPo.name
                    });

                    return userPermissions;
                },
                new
                {
                    NodeId = node.Id
                });

            return userPermissionsLookup.Values.ToArray();
        }
    }
}
