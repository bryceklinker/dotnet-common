using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs.Common
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services, CqrsOptions options)
        {
            var locator = new RegistrationLocator();
            foreach (var registration in locator.GetRegistrations(options.Assemblies))
                services.AddTransient(registration.InterfaceType, registration.ImplementationType);
            services.AddTransient(p => CreateBus(p, options));
            return services;
        }

        private static ICqrsBus CreateBus(IServiceProvider provider, CqrsOptions options)
        {
            ICqrsBus bus = new KlinkedCqrsBus(provider);
            var decoratedBus = options.DecoratorFactories
                .Aggregate(bus, (current, factory) => factory(current));
            
            return options.ProviderDecoratorFactories
                .Aggregate(decoratedBus, (current, factory) => factory(provider, current));
        }
    }
}