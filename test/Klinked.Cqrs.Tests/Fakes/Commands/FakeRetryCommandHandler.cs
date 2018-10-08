using System;
using System.Threading.Tasks;
using Klinked.Cqrs.Commands;

namespace Klinked.Cqrs.Tests.Fakes.Commands
{
    public class FakeRetryCommandArgs
    {
        public int TimesExecuted { get; private set; }

        public int TimesToFail { get; }

        public FakeRetryCommandArgs(int timesToFail)
        {
            TimesToFail = timesToFail;
        }

        public bool ShouldFail()
        {
            return TimesToFail > TimesExecuted++;
        }
    }
    
    public class FakeRetryCommandHandler : ICommandHandler<FakeRetryCommandArgs>
    {
        public Task Execute(FakeRetryCommandArgs args)
        {
            if (args.ShouldFail())
                throw new Exception();
            
            return Task.CompletedTask;
        }
    }
}