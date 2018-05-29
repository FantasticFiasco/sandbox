﻿using System;
using ConsoleInDocker;
using Microsoft.Extensions.DependencyInjection;
using Paramore.Brighter;
using Paramore.Brighter.Policies.Handlers;
using Polly;

namespace RussianDoll
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = BuildServiceProvider();
            var commandProcessor = BuildCommandProcessor(serviceProvider);

            commandProcessor.Send(new TargetCommand());

            Wait.ForShutdown();
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddScoped<TargetHandler>();
            serviceCollection.AddScoped(typeof(StepsAndTimingHandler<>));
            
            return serviceCollection.BuildServiceProvider();
        }
        
        private static IAmACommandProcessor BuildCommandProcessor(IServiceProvider serviceProvider)
        {
            var registry = CreateRegistry(); 
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

            registry.Register<TargetCommand, TargetHandler>();
            
            return registry;
        }
    }
}
