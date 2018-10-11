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
            return services.AddTransient<ICqrsBus>(p => new KlinkedCqrsBus(p, options));
        }
    }
}