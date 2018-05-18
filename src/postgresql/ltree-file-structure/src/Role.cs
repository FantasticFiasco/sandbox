using System.Collections.Generic;

namespace FileSystem
{
    public class Role
    {
        public Role(string id, string name)
        {
            Id = id;
            Name = name;
            Operations = new List<Operation>();
        }

        public string Id { get; }

        public string Name { get; }

        public List<Operation> Operations { get; }
    }
}
