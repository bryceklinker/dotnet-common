using System;

namespace Klinked.Gherkin.Steps
{
    [AttributeUsage(AttributeTargets.Method)]
    public class WhenAttribute : StepAttribute
    {
        public WhenAttribute(string regex)
            : base(regex)
        {
            
        }
    }
}