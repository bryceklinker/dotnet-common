using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Klinked.Gherkin.Common
{
    internal static class TypeExtensions
    {
        public static IEnumerable<MethodInfo> GetMethodsWithStepAttributes(this Type type)
        {
            return type.GetMethods()
                .Where(m => m.HasStepAttributes());
        }
    }
}