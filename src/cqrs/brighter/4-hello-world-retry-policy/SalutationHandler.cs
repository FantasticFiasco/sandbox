using System;
using Paramore.Brighter;
using Paramore.Brighter.Policies.Attributes;
using Paramore.Brighter.Policies.Handlers;

namespace HelloWorld.RetryPolicy
{
    public class SalutationHandler : MyRequestHandler<SalutationCommand>, IDisposable
    {
        private int attempt = 0;
        
        public SalutationHandler()
        {
            Console.WriteLine($"{GetType().FullName}.ctor");
        }

        [FallbackPolicy(step: 1, backstop: true, circuitBreaker: false)]
        [UsePolicy(step: 2, policy: "GreetingRetryPolicy")]
        public override SalutationCommand Handle(SalutationCommand command)
        {
            string greeting = GetGreeting(command);            
            
            ThrowOnTheDarkLord(command);
            
            Console.WriteLine(greeting);

            return base.Handle(command);
        }

        public override SalutationCommand ExceptionFallback(SalutationCommand command, Exception exception)
        {
            Console.WriteLine($"Still failed after {attempt} attempts.");
            Console.WriteLine(exception);

            return base.ExceptionFallback(command, exception);
        }

        public void Dispose()
        {
            Console.WriteLine("I'm being disposed.");
        }

        private string GetGreeting(SalutationCommand command)
        {
            ThrowOnFailureToRetrieveGreeting(command);

            return $"Hello, {command.Name}";
        }

        private void ThrowOnFailureToRetrieveGreeting(SalutationCommand command)
        {
            attempt++;

            int numFailures = Math.Max((command.Name.Length - 3) / 2, 0);

            if (attempt % (numFailures + 1) != 0)
            {
                throw new ApplicationException($"While trying to greet {command.Name} a failure happend. I'm on attempt {attempt}, and I will fail {numFailures} times.");
            }
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