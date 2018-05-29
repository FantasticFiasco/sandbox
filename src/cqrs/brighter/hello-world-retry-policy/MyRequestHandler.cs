using System;
using Paramore.Brighter;
using Paramore.Brighter.Policies.Handlers;

namespace HelloWorld.RetryPolicy
{
    public class MyRequestHandler<TCommand> : RequestHandler<TCommand> where TCommand : class, IRequest
    {
        public override TCommand Fallback(TCommand command)
        {
            if (this.Context.Bag.TryGetValue(
                FallbackPolicyHandler<TCommand>.CAUSE_OF_FALLBACK_EXCEPTION,
                out object exception))
                {
                    return base.Fallback(ExceptionFallback(command, (Exception)exception));    
                }

            return base.Fallback(command);
        }

        public virtual TCommand ExceptionFallback(TCommand command, Exception exception)
        {
            // If exceptions need to be handled, this should be implemented in a derived class
            return command;
        }
    } 
}