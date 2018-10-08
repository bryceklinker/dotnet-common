using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Klinked.Gherkin.Common
{
    internal static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetPublicTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes().Where(t => t.IsPublic);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Type>();
            }
        }
    }
}