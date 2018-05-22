using System.Collections.Generic;

namespace FileSystem
{
    public class Role
    {
        public Role(string id, string name, Reference inheritedFrom)
        {
            Id = id;
            Name = name;
            Operations = new List<Operation>();
            InheritedFrom = inheritedFrom;
        }

        public string Id { get; }

        public string Name { get; }

        public List<Operation> Operations { get; }

        public Reference InheritedFrom { get; }
    }
}
