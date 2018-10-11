using System;
using System.Threading.Tasks;
using Klinked.Cqrs.Tests.Fakes;
using Klinked.Cqrs.Tests.Fakes.Queries;
using Klinked.Cqrs.Retry;
using Klinked.Cqrs.Tests.Fakes.Commands;
using Klinked.Cqrs.Tests.Fakes.Events;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Xunit;

namespace Klinked.Cqrs.Tests
{
    public class RetryTests : IDisposable
    {
        private readonly ICqrsBus _bus;

        public RetryTests()
        {
            _bus = CqrsBus
                .UseAssemblyFor<FakeLogger>()
                .AddRetry()
                .Build();
        }

        [Fact]
        public async Task ShouldRetryQueryThreeTimes()
        {
            var args = new FakeRetryQueryArgs(2, new []{4,3});
            
            var result = await _bus.Execute<FakeRetryQueryArgs, int[]>(args);
            Assert.Equal(args.Result, result);
            Assert.Equal(3, args.TimesExecuted);
        }

        [Fact]
        public async Task ShouldRetryCommandThreeTimes()
        {
            var args = new FakeRetryCommandArgs(2);

            await _bus.Execute(args);
            Assert.Equal(3, args.TimesExecuted);
        }

        [Fact]
        public async Task ShouldRetryEventThreeTimes()
        {
            var args = new FakeRetryEventArgs(2);

            await _bus.Publish(args);
            Assert.Equal(4, args.NumberOfAttemptsToHandle);
        }

        [Fact]
        public async Task ShouldOnlyRetryFailedEvents()
        {
            var args = new FakeRetryEventArgs(2);

            await _bus.Publish(args);
            Assert.Equal(2, args.NumberOfSuccessfulHandles);
        }

        [Fact]
        public async Task ShouldAllowUsageOfCustomRetryPolicy()
        {
            var bus = CqrsBus
                .UseAssemblyFor<FakeLogger>()
                .AddRetry(Policy.Handle<Exception>().RetryAsync(12))
                .Build();

            var args = new FakeRetryCommandArgs(11);
            await bus.Execute(args);

            Assert.Equal(12, args.TimesExecuted);
        }

        [Fact]
        public async Task ShouldWorkCorrectlyWithServiceCollection()
        {
            var bus = new ServiceCollection()
                .AddKlinkedCqrs(b => b.UseAssemblyFor<FakeLogger>().AddRetry())
                .BuildServiceProvider()
                .GetRequiredService<ICqrsBus>();

            var args = new FakeRetryCommandArgs(2);
            await bus.Execute(args);
            
            Assert.Equal(3, args.TimesExecuted);
        }

        [Fact]
        public async Task ShouldWorkCorrectlyWithCustomPolicyAndServiceCollection()
        {
            var bus = new ServiceCollection()
                .AddKlinkedCqrs(b => b.UseAssemblyFor<FakeLogger>().AddRetry(Policy.Handle<Exception>().RetryAsync(12)))
                .BuildServiceProvider()
                .GetRequiredService<ICqrsBus>();

            var args = new FakeRetryCommandArgs(11);
            await bus.Execute(args);
            
            Assert.Equal(12, args.TimesExecuted);
        }

        [Fact]
        public async Task ShouldThrowExceptionIfRetriesExceeded()
        {
            var bus = CqrsBus.UseAssemblyFor<FakeLogger>().AddRetry().Build();

            var args = new FakeRetryCommandArgs(4);
            await Assert.ThrowsAsync<Exception>(() => _bus.Execute(args));
        }
        
        public void Dispose()
        {
            FakeRetryFailuresEventHandler.TimesHandled = 0;
        }
    }
}