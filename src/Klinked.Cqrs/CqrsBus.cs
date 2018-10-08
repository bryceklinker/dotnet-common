using System.Reflection;

namespace Klinked.Cqrs
{
    public static class CqrsBus
    {
        public static ICqrsBusBuilder UseAssembly(Assembly assembly)
        {
            var builder = new CqrsBusBuilder(new CqrsOptionsBuilder());
            return builder.UseAssembly(assembly);
        }

        public static ICqrsBusBuilder UseAssemblyFor<T>()
        {
            return UseAssembly(typeof(T).Assembly);
        }
    }
}