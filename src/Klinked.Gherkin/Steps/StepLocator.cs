using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Klinked.Gherkin.Common;

namespace Klinked.Gherkin.Steps
{
    internal class StepLocator
    {
        private static readonly Lazy<StepLocator> Locator = new Lazy<StepLocator>(() => new StepLocator());
        private StepDefinition[] _steps;
        
        public static StepLocator Instance => Locator.Value;
        
        public StepDefinition[] GetDefinitions()
        {
            if (_steps != null)
                return _steps;

            return _steps = LocateStepDefinitions();
        }

        private StepDefinition[] LocateStepDefinitions()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetPublicTypes())
                .SelectMany(t => t.GetMethodsWithStepAttributes())
                .SelectMany(CreateStepDefinitionsForMethod)
                .ToArray();
        }

        private IEnumerable<StepDefinition> CreateStepDefinitionsForMethod(MethodInfo method)
        {
            return method.GetStepAttributes()
                .Select(a => new StepDefinition(method.DeclaringType, method, a));
        }
    }
}