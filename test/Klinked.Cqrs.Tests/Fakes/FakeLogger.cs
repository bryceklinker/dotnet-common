using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Klinked.Cqrs.Tests.Fakes
{
    public class FakeLogger : ILogger, IDisposable
    {
        private readonly Dictionary<LogLevel, List<string>> _messages;

        public FakeLogger()
        {
            _messages = Enum.GetValues(typeof(LogLevel))
                .Cast<LogLevel>()
                .ToDictionary(l => l, l => new List<string>());
        }

        public string[] GetMessages(LogLevel level)
        {
            return _messages[level].ToArray();
        }
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            _messages[logLevel].Add(message);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}