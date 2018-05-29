using System;
using ConsoleInDocker;
using Microsoft.Extensions.DependencyInjection;
using Paramore.Brighter;

namespace HelloWorld.Scoped
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();
            var commandProcessor = BuildCommandProcessor(serviceProvider);

            commandProcessor.Send(new SalutationCommand("John"));
            commandProcessor.Send(new SalutationCommand("Jane"));

            Wait.ForShutdown();
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<SalutationHandler>();
            
            return serviceCollection.BuildServiceProvider();
        }

        private static IAmACommandProcessor BuildCommandProcessor(IServiceProvider serviceProvider)
        {
            // 1. Maps commands to Handlers
            var registry = CreateRegistry();
            
            // 2. Builds handlers
            var factory = new ServiceProviderHandler(serviceProvider);

            var builder = CommandProcessorBuilder
                .With()
                .Handlers(new HandlerConfiguration(
                    subscriberRegistry: registry,
                    handlerFactory: factory))
                .DefaultPolicy()
                .NoTaskQueues()
                .RequestContextFactory(new InMemoryRequestContextFactory());

            return builder.Build();
        }

        private static SubscriberRegistry CreateRegistry()
        {
            var registry = new SubscriberRegistry();

            registry.Register<SalutationCommand, SalutationHandler>();
            
            return registry;
        }
    }
}
