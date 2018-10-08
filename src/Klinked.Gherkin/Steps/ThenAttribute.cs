using System;

namespace Klinked.Gherkin.Steps
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ThenAttribute : StepAttribute
    {
        public ThenAttribute(string regex)
            : base(regex)
        {
            
        }
    }
}