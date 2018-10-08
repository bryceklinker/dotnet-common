using System.Threading.Tasks;
using Klinked.Cqrs.Commands;

namespace Klinked.Cqrs.Tests.Fakes.Commands
{
    public class FakeCommandWithServiceArgs
    {
        public ISomeService Service { get; set; }
        public bool DidExecute { get; set; }
    }
    
    public class FakeCommandWithServiceHandler : ICommandHandler<FakeCommandWithServiceArgs>
    {
        private readonly ISomeService _service;

        public FakeCommandWithServiceHandler(ISomeService service)
        {
            _service = service;
        }

        public Task Execute(FakeCommandWithServiceArgs args)
        {
            args.Service = _service;
            args.DidExecute = true;
            return Task.CompletedTask;
        }
    }

    public interface ISomeService
    {
        
    }

    public class SomeService : ISomeService
    {
        
    }
}