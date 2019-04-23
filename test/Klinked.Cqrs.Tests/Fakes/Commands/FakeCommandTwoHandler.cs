using System.Threading.Tasks;
using Klinked.Cqrs.Commands;

namespace Klinked.Cqrs.Tests.Fakes.Commands
{
    public class FakeCommandTwoArgs
    {
        public bool DidExecute { get; set; }
    }
    
    public class FakeCommandTwoHandler : ICommandHandler<FakeCommandTwoArgs>
    {
        public Task ExecuteAsync(FakeCommandTwoArgs args)
        {
            args.DidExecute = true;
            return Task.CompletedTask;
        }
    }
}