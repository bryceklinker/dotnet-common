using System;

namespace Klinked.Gherkin.Steps
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GivenAttribute : StepAttribute
    {
        public GivenAttribute(string regex)
            : base(regex)
        {
            
        }
    }
}