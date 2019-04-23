using System.Threading.Tasks;
using Klinked.Cqrs.Logging;
using Klinked.Cqrs.Retry;
using Klinked.Cqrs.Tests.Fakes;
using Klinked.Cqrs.Tests.Fakes.Commands;
using Klinked.Cqrs.Tests.Fakes.Events;
using Klinked.Cqrs.Tests.Fakes.Queries;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Klinked.Cqrs.Tests
{
    public class DecorationTests
    {
        private readonly FakeLogger _logger;
        private readonly ICqrsBus _bus;

        public DecorationTests()
        {
            var loggerFactory = new FakeLoggerFactory();
            _logger = loggerFactory.Logger;
            
            _bus = CqrsBus.UseAssemblyFor<FakeLogger>()
                .AddLogging(loggerFactory)
                .AddRetry()
                .Build();
        }

        [Fact]
        public async Task ShouldUseRetryAndLoggingQueryDecorators()
        {
            var args = new FakeRetryQueryArgs(2, new int[0]);
            await _bus.ExecuteAsync<FakeRetryQueryArgs, int[]>(args);
            Assert.Equal(3, args.TimesExecuted);
            Assert.Equal(4, _logger.GetMessages(LogLevel.Information).Length);
        }

        [Fact]
        public async Task ShouldUseRetryAndLoggingCommandDecorators()
        {
            var args = new FakeRetryCommandArgs(2);
            await _bus.ExecuteAsync(args);
            Assert.Equal(3, args.TimesExecuted);
            Assert.Equal(4, _logger.GetMessages(LogLevel.Information).Length);
        }

        [Fact]
        public async Task ShouldUseRetryAndLoggingEventDecorators()
        {
            var args = new FakeRetryEventArgs(2);
            await _bus.PublishAsync(args);
            Assert.Equal(4, args.NumberOfAttemptsToHandle);
            Assert.Equal(6, _logger.GetMessages(LogLevel.Information).Length);
        }
    }
}