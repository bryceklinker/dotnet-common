using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Klinked.Gherkin.Steps;

namespace Klinked.Gherkin.Common
{
    internal static class MethodExtensions
    {
        public static bool IsAsync(this MethodInfo method)
        {
            return method.ReturnType == typeof(Task)
                   || method.ReturnType.IsGenericType &&
                   method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
        }
        
        public static bool HasStepAttributes(this MethodInfo method)
        {
            return method.GetStepAttributes().Any();
        }

        public static IEnumerable<StepAttribute> GetStepAttributes(this MethodInfo method)
        {
            return method.GetCustomAttributes().OfType<StepAttribute>();
        }
    }
}