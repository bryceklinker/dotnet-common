using System.Threading.Tasks;
using Klinked.Cqrs.Logging;
using Klinked.Cqrs.Tests.Fakes;
using Klinked.Cqrs.Tests.Fakes.Commands;
using Klinked.Cqrs.Tests.Fakes.Events;
using Klinked.Cqrs.Tests.Fakes.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Klinked.Cqrs.Tests
{
    public class LoggingTests
    {
        private readonly FakeLogger _logger;
        private readonly ICqrsBus _bus;

        public LoggingTests()
        {
            var factory = new FakeLoggerFactory();
            _logger = factory.Logger;
            _bus = CqrsBus
                .UseAssemblyFor<FakeLogger>()
                .AddLogging(factory)
                .Build();
        }

        [Fact]
        public async Task ShouldLogInfoWhenExecutingACommand()
        {
            var args = new FakeCommandArgs();
            await _bus.ExecuteAsync(args);
            
            Assert.True(args.DidExecute);
            Assert.Equal(2, _logger.GetMessages(LogLevel.Information).Length);
        }
        
        [Fact]
        public async Task ShouldLogInfoWhenExecutingQuery()
        {
            var args = new FakeQueryArgs(new[]{1, 5});
            var actual  = await _bus.ExecuteAsync<FakeQueryArgs, int[]>(args);

            Assert.Equal(args.Result, actual);
            Assert.Equal(2, _logger.GetMessages(LogLevel.Information).Length);
        }
        
        [Fact]
        public async Task ShouldLogInfoWhenPublishingEvent()
        {
            var args = new FakeEventArgs();

            await _bus.PublishAsync(args);
            Assert.Equal(2, args.TimesHandled);
            Assert.Equal(4, _logger.GetMessages(LogLevel.Information).Length);
        }
        
        [Fact]
        public void ShouldWorkCorrectlyWithServiceCollectionConfiguration()
        {
            var bus = new ServiceCollection()
                .AddLogging()
                .AddKlinkedCqrs(b => b.AddLogging().UseAssemblyFor<FakeLogger>())
                .BuildServiceProvider()
                .GetRequiredService<ICqrsBus>();

            Assert.NotNull(bus);
        }
    }
}