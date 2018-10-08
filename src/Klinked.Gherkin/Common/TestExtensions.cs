using System.Reflection;
using Klinked.Gherkin.Scenarios;
using Xunit.Abstractions;

namespace Klinked.Gherkin.Common
{
    internal static class TestExtensions
    {
        public static string GetScenarioName(this ITest test)
        {
            var method = test.TestCase.TestMethod.Method;
            var scenarioAttribute = method.ToRuntimeMethod()
                .GetCustomAttribute<ScenarioAttribute>();
            return scenarioAttribute?.Name ?? "Unknown Scenario";
        }
    }
}