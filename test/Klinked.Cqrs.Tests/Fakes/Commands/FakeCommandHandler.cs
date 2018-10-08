using System.Threading.Tasks;
using Klinked.Cqrs.Commands;

namespace Klinked.Cqrs.Tests.Fakes.Commands
{
    public class FakeCommandArgs
    {
        public bool DidExecute { get; set; }
    }
    
    public class FakeCommandHandler : ICommandHandler<FakeCommandArgs>
    {
        public Task Execute(FakeCommandArgs args)
        {
            args.DidExecute = true;
            return Task.CompletedTask;
        }
    }
}