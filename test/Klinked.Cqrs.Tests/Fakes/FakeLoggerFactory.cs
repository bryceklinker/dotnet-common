using Microsoft.Extensions.Logging;

namespace Klinked.Cqrs.Tests.Fakes
{
    public class FakeLoggerFactory : ILoggerFactory
    {
        public FakeLogger Logger { get; } = new FakeLogger();

        public ILogger CreateLogger(string categoryName)
        {
            return Logger;
        }

        public void AddProvider(ILoggerProvider provider)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}