using System;
using Paramore.Brighter;

namespace HelloWorld
{
    public class SimpleHandlerFactory : IAmAHandlerFactory
    {
        public IHandleRequests Create(Type handlerType)
        {
            if (handlerType == typeof(GreetingCommandHandler))
            {
                return new GreetingCommandHandler();
            }

            // Ignore other handler types for demo
            return null;
        }

        public void Release(IHandleRequests handler)
        {
        }
    }
}