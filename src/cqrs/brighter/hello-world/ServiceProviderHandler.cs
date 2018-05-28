using System;
using Paramore.Brighter;

namespace HelloWorld
{
    public class ServiceProviderHandler : IAmAHandlerFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceProviderHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        public IHandleRequests Create(Type handlerType)
        {
            return (IHandleRequests)serviceProvider.GetService(handlerType);
        }

        public void Release(IHandleRequests handler)
        {
        }
    }
}