using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs
{
    public static class CqrsBus
    {
        public static ICqrsBusBuilder UseAssembly(Assembly assembly, IServiceCollection services = null)
        {
            var builder = new CqrsBusBuilder(new CqrsOptionsBuilder(services));
            return builder.UseAssembly(assembly);
        }

        public static ICqrsBusBuilder UseAssemblyFor<T>(IServiceCollection services = null)
        {
            return UseAssembly(typeof(T).Assembly, services);
        }
    }
}