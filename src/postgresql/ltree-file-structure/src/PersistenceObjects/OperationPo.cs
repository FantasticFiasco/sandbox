namespace FileSystem.PersistenceObjects
{
    public class OperationPo
    {
        public string id { get; set; }

        public string name { get; set; }

        public string role_id { get; set; }

        public override string ToString() => $"id: {id}; name: {name}; role id: {role_id}";
    }
}
