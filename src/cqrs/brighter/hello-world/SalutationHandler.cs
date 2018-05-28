using System;
using Paramore.Brighter;

namespace HelloWorld
{
    public class SalutationHandler : RequestHandler<SalutationCommand>
    {
        public override SalutationCommand Handle(SalutationCommand command)
        {
            Console.WriteLine($"Greetings, {command.Name}.");
            
            return base.Handle(command);
        }
    }
}