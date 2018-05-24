using System;
using Paramore.Brighter;

namespace HelloWorld
{
    public class GreetingCommand : IRequest
    {
        public GreetingCommand(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; set; }

        public string Name { get; }
    }
}