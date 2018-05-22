namespace FileSystem.PersistenceObjects
{
    public class UserPermissionPo
    {
        public string user_id { get; set; }

        public string node_id { get; set; }

        public string role_id { get; set; }

        public override string ToString() => $"user id: {user_id}; node id: {node_id}; role id: {role_id}";
    }
}
