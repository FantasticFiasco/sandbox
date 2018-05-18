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
                @"SELECT up.*, r.*, o.*
                FROM user_permission up
                JOIN role r ON r.id = up.role_id
                JOIN operation o ON o.role_id = r.id
                WHERE up.node_id = @NodeId",
                (userPermissionPo, rolePo, operationPo) =>
                {
                    if (!userPermissionsLookup.TryGetValue(userPermissionPo.user_id, out var userPermissions))
                    {
                        userPermissions = new UserPermissions(userPermissionPo.user_id);

                        userPermissionsLookup.Add(userPermissions.UserId, userPermissions);
                    }

                    if (!roleLookup.TryGetValue(rolePo.id, out var role))
                    {
                        role = new Role(rolePo.id, rolePo.name);
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
