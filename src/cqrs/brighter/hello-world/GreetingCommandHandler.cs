using System;
using Paramore.Brighter;

namespace HelloWorld
{
    public class GreetingCommandHandler : RequestHandler<GreetingCommand>
    {
        public override GreetingCommand Handle(GreetingCommand command)
        {
            Console.WriteLine($"Hello {command.Name}");

            return base.Handle(command);
        }
    }
}