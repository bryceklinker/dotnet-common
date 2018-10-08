using System.Collections.Generic;
using System.Reflection;
using Xunit.Abstractions;

namespace Klinked.Gherkin.Tests.Fakes
{
    public class FakeTestOutputHelper : ITestOutputHelper
    {
        private ITest test;
        private List<string> _messages = new List<string>();

        public string[] Messages => _messages.ToArray();
        public string AllText => string.Join("", Messages);

        public FakeTestOutputHelper(ITestOutputHelper output)
        {
            var type = output.GetType();
            var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
            test = (ITest)testMember.GetValue(output);
        }

        public void WriteLine(string message)
        {
            _messages.Add(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            _messages.Add(string.Format(format, args));
        }
    }
}