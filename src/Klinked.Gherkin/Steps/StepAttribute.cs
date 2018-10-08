using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Klinked.Gherkin.Steps
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StepAttribute : Attribute
    {
        public Regex Regex { get; }
        
        public StepAttribute(string regex)
        {
            Regex = new Regex(regex);
        }

        public bool IsMatch(string text)
        {
            return Regex.IsMatch(text);
        }

        public string[] GetParameters(string text)
        {
            var matches = Regex.Matches(text);
            return matches
                .Cast<Match>()
                .SelectMany(m => m.Groups.Cast<Group>())
                .Skip(1)
                .Select(g => g.Value)
                .ToArray();
        }
    }
}