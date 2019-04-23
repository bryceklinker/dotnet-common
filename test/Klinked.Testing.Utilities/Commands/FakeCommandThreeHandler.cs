using System.Threading.Tasks;
using Klinked.Cqrs.Commands;

namespace Klinked.Testing.Utilities.Commands
{
    public class FakeCommandThreeArgs
    {
        public bool DidExecute { get; set; }    
    }
    
    public class FakeCommandThreeHandler : ICommandHandler<FakeCommandThreeArgs>
    {
        public Task ExecuteAsync(FakeCommandThreeArgs args)
        {
            args.DidExecute = true;
            return Task.CompletedTask;
        }
    }
}