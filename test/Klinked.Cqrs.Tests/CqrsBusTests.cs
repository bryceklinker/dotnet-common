using System;
using System.Threading.Tasks;
using Klinked.Cqrs.Tests.Fakes;
using Klinked.Cqrs.Tests.Fakes.Commands;
using Klinked.Cqrs.Tests.Fakes.Events;
using Klinked.Cqrs.Tests.Fakes.Queries;
using Klinked.Testing.Utilities.Commands;
using Klinked.Testing.Utilities.Queries;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Klinked.Cqrs.Tests
{
    public class CqrsBusTests : IDisposable
    {
        private readonly ICqrsBus _bus;
        
        public CqrsBusTests()
        {
            _bus = CqrsBus.UseAssemblyFor<FakeCommandArgs>()
                .UseAssemblyFor<FakeCommandThreeArgs>()
                .Build();
        }
        
        [Fact]
        public async Task ShouldExecuteCommand()
        {
            var args = new FakeCommandArgs();
            await _bus.ExecuteAsync(args);
            Assert.True(args.DidExecute);
        }
        
        [Fact]
        public async Task ShouldExecuteCommandForDifferentCommandInTheSameAssembly()
        {
            var args = new FakeCommandTwoArgs();
            await _bus.ExecuteAsync(args);
            Assert.True(args.DidExecute);
        }
        
        [Fact]
        public async Task ShouldExecuteCommandFromADifferentAssembly()
        {
            var args = new FakeCommandThreeArgs();
            await _bus.ExecuteAsync(args);
            Assert.True(args.DidExecute);
        }

        [Fact]
        public async Task ShouldExecuteCommandWhenClassImplementsQueryAndCommand()
        {
            var args = new Model {Id = Guid.NewGuid()};

            await _bus.ExecuteAsync(args);
            Assert.Contains(args, Repository.Models);
        }

        [Fact]
        public async Task ShouldReturnQueryResults()
        {
            var args = new FakeQueryArgs(new[] {4, 6, 8});
            var actual = await _bus.ExecuteAsync<FakeQueryArgs, int[]>(args);
            Assert.Equal(args.Result, actual);
        }

        [Fact]
        public async Task ShouldReturnQueryResultsForDifferentQueryInTheSameAssembly()
        {
            var args = new FakeQueryTwoArgs(Guid.NewGuid());

            var actual = await _bus.ExecuteAsync<FakeQueryTwoArgs, Guid>(args);
            Assert.Equal(args.Result, actual);
        }

        [Fact]
        public async Task ShouldReturnQueryResultsFromQueryInDifferentAssembly()
        {
            var args = new FakeQueryThreeArgs(Guid.NewGuid().ToByteArray());

            var actual = await _bus.ExecuteAsync<FakeQueryThreeArgs, byte[]>(args);
            Assert.Equal(args.Result, actual);
        }
        
        [Fact]
        public async Task ShouldReturnQueryResultsWhenClassImplementsQueryAndCommand()
        {
            var model = new Model {Id = Guid.NewGuid()};
            await _bus.ExecuteAsync(model);

            var actual = await _bus.ExecuteAsync<Guid, Model>(model.Id);
            Assert.Equal(model, actual);
        }

        [Fact]
        public async Task ShouldPublishEventToAllHandlers()
        {
            var args = new FakeEventArgs();
            await _bus.PublishAsync(args);

            Assert.Equal(2, args.TimesHandled);
        }
        
        [Fact]
        public async Task ShouldCreateBusWithoutServiceCollection()
        {
            var bus = CqrsBus.UseAssembly(typeof(FakeCommandArgs).Assembly).Build();
            
            var args = new FakeCommandArgs();
            await bus.ExecuteAsync(args);
            Assert.True(args.DidExecute);
        }
        
        [Fact]
        public async Task ShouldCreateBusUsingProvidedOptions()
        {
            var bus = new ServiceCollection()
                .AddKlinkedCqrs(new CqrsOptions
                {
                    Assemblies = new []{ typeof(FakeCommandThreeArgs).Assembly }
                })
                .BuildServiceProvider()
                .GetRequiredService<ICqrsBus>(); 
            
            var args = new FakeCommandThreeArgs();

            await bus.ExecuteAsync(args);
            Assert.True(args.DidExecute);
        }
        
        [Fact]
        public async Task ShouldUseCommandsForAssemblyContainingType()
        {
            var bus = CqrsBus.UseAssemblyFor<FakeLogger>()
                .Build();

            var args = new FakeCommandArgs();

            await bus.ExecuteAsync(args);
            Assert.True(args.DidExecute);
        }
        
        [Fact]
        public async Task ShouldAllowExternalServicesToBeProvided()
        {
            var instance = new SomeService();
            var bus = CqrsBus.UseAssemblyFor<FakeCommandWithServiceArgs>()
                .AddSingleton<ISomeService>(instance)
                .Build();
            
            var args = new FakeCommandWithServiceArgs();
            await bus.ExecuteAsync(args);
            Assert.Equal(instance, args.Service);
            Assert.True(args.DidExecute);
        }
        
        public void Dispose()
        {
            Repository.Clear();
        }
    }
}