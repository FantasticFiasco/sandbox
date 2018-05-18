namespace FileSystem.PersistenceObjects
{
    public class RolePo
    {
        public string id { get; set; }

        public string name { get; set; }

        public override string ToString() => $"id: {id}; name: {name}";
    }
}
