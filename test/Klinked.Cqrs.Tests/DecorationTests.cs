using System;
using System.Threading.Tasks;
using Klinked.Cqrs.Tests.Fakes;
using Klinked.Cqrs.Tests.Fakes.Commands;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Klinked.Cqrs.Tests
{
    public class DecorationTests
    {
        private readonly FakeLogger _logger;
        private readonly ICqrsBusBuilder _builder;

        public DecorationTests()
        {
            _logger = new FakeLogger();
            _builder = CqrsBus
                .UseAssemblyFor<FakeCommandArgs>();
        }
        
        [Fact]
        public async Task ShouldLogMessagesWhenExecutingCommand()
        {
            var bus = _builder
                .UseDecoratorFactory(b => new TraceDecorator(b, _logger))
                .Build();
            
            var args = new FakeCommandArgs();

            await bus.Execute(args);
            Assert.Equal(2, _logger.GetMessages(LogLevel.Trace).Length);
        }
        
        [Fact]
        public async Task ShouldAllowUsageOfMultipleDecorators()
        {
            var bus = _builder
                .UseDecoratorFactory(b => new TraceDecorator(b, _logger))
                .UseDecoratorFactory(b => new DebugDecorator(b, _logger))
                .Build();
            
            var args = new FakeCommandArgs();

            await bus.Execute(args);
            Assert.Equal(2, _logger.GetMessages(LogLevel.Debug).Length);
            Assert.Equal(2, _logger.GetMessages(LogLevel.Trace).Length);
        }
        
        [Fact]
        public void ShouldAllowDecorationWithServiceProvider()
        {
            var bus = _builder
                .UseDecoratorFactory((provider, b) => new ProviderDecorator(provider, b))
                .Build() as ProviderDecorator;
            
            Assert.NotNull(bus.Provider);
        }
        
        [Fact]
        public async Task ShouldAllowDecoratorsFlavorsToBeCombined()
        {
            var bus = _builder
                .UseDecoratorFactory(b => new TraceDecorator(b, _logger))
                .UseDecoratorFactory((p, b) => new ProviderDecorator(p, b))
                .UseDecoratorFactory(b => new DebugDecorator(b, _logger))
                .Build();
            
            var args = new FakeCommandArgs();
            await bus.Execute(args);
            
            Assert.Equal(2, _logger.GetMessages(LogLevel.Trace).Length);
            Assert.Equal(2, _logger.GetMessages(LogLevel.Debug).Length);
        }
    }

    public class DebugDecorator : ICqrsBus
    {
        private readonly ICqrsBus _original;
        private readonly ILogger _logger;

        public DebugDecorator(ICqrsBus original, ILogger logger)
        {
            _original = original;
            _logger = logger;
        }

        public async Task Execute<TCommandArgs>(TCommandArgs args)
        {
            _logger.LogDebug("Executing");
            await _original.Execute(args);
            _logger.LogDebug("Executed");
        }

        public Task<TResult> Execute<TArgs, TResult>(TArgs args)
        {
            throw new System.NotImplementedException();
        }

        public Task Publish<TArgs>(TArgs args)
        {
            throw new NotImplementedException();
        }
    }
    
    public class TraceDecorator : ICqrsBus
    {
        private readonly ICqrsBus _original;
        private readonly ILogger _logger;

        public TraceDecorator(ICqrsBus original, ILogger logger)
        {
            _original = original;
            _logger = logger;
        }

        public async Task Execute<TCommandArgs>(TCommandArgs args)
        {
            _logger.LogTrace("Executing");
            await _original.Execute(args);
            _logger.LogTrace("Executed");
        }

        public Task<TResult> Execute<TArgs, TResult>(TArgs args)
        {
            throw new System.NotImplementedException();
        }

        public Task Publish<TArgs>(TArgs args)
        {
            throw new NotImplementedException();
        }
    }

    public class ProviderDecorator : ICqrsBus
    {
        private readonly ICqrsBus _bus;
        public IServiceProvider Provider { get; }

        public ProviderDecorator(IServiceProvider provider, ICqrsBus bus)
        {
            Provider = provider;
            _bus = bus;
        }

        public Task Execute<TCommandArgs>(TCommandArgs args)
        {
            return _bus.Execute(args);
        }

        public Task<TResult> Execute<TArgs, TResult>(TArgs args)
        {
            throw new System.NotImplementedException();
        }

        public Task Publish<TArgs>(TArgs args)
        {
            throw new NotImplementedException();
        }
    }
}