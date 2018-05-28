using System;
using Paramore.Brighter;
using Paramore.Brighter.Policies.Attributes;
using Paramore.Brighter.Policies.Handlers;

namespace HelloWorld.FallbackHandler
{
    public class SalutationHandler : MyRequestHandler<SalutationCommand>, IDisposable
    {
        [FallbackPolicy(backstop: true, circuitBreaker: false, step: 1)]
        public override SalutationCommand Handle(SalutationCommand command)
        {
            Console.WriteLine($"Greetings, {command.Name}.");
        
            ThrowOnTheDarkLord(command);
    
            return base.Handle(command);
        }

        public override SalutationCommand ExceptionFallback(SalutationCommand command, Exception exception)
        {
            Console.WriteLine(exception);

            return base.ExceptionFallback(command, exception);
        }

        public void Dispose()
        {
            Console.WriteLine("I'm being disposed.");
        }

        private static void ThrowOnTheDarkLord(SalutationCommand command)
        {
            if (command.Name == "Voldemort")
            {
                throw new ApplicationException("A death-eater has appeared.");
            }
        }
    }
}