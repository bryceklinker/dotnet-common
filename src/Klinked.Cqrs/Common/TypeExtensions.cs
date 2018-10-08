using System;
using System.Linq;

namespace Klinked.Cqrs.Common
{
    internal static class TypeExtensions
    {
        public static bool ImplementsInterface(this Type type, Type interfaceType)
        {
            if (interfaceType.IsGenericType)
                return type.ImplementsGenericInterface(interfaceType);
            return false;
        }

        private static bool ImplementsGenericInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                .Where(it => it.IsGenericType)
                .Any(it => it.GetGenericTypeDefinition() == interfaceType);
        }
    }
}