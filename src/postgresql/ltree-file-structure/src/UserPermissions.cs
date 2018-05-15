using System;
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
            throw new NotImplementedException();
        }

        public UserPermission[] GetForNode(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
