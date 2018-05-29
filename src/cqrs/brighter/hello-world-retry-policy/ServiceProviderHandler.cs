using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Paramore.Brighter;

namespace HelloWorld.RetryPolicy
{
    public class ServiceProviderHandler : IAmAHandlerFactory
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ConcurrentDictionary<IHandleRequests, IServiceScope> activeHandlers;
        
        public ServiceProviderHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            activeHandlers = new ConcurrentDictionary<IHandleRequests, IServiceScope>();
        }

        public IHandleRequests Create(Type handlerType)
        {
            IServiceScope scope = serviceProvider.CreateScope();            
            IServiceProvider scopedProvider = scope.ServiceProvider;
            IHandleRequests result = (IHandleRequests)scopedProvider.GetService(handlerType);
            
            if (activeHandlers.TryAdd(result, scope))
            {
                return result;
            }

            scope.Dispose();

            throw new InvalidOperationException("The handler could not be tracked properly. It may be declared in the service collection with the wrong lifecyle.");
        }

        public void Release(IHandleRequests handler)
        {
            if (activeHandlers.TryRemove(handler, out IServiceScope scope))
            {
                scope.Dispose();
            }
        }
    }
}