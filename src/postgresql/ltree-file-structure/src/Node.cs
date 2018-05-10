namespace FileSystem
{
    public class Node
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name}; Path: {Path}";
        }
    }
}
