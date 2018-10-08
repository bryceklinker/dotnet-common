using System;

namespace Klinked.Gherkin.Scenarios
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ScenarioAttribute : Attribute
    {
        public string Name { get; }

        public ScenarioAttribute(string name)
        {
            Name = name;
        }
    }
}