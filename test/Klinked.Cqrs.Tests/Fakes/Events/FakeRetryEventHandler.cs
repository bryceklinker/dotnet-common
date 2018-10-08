using System;
using System.Threading.Tasks;
using Klinked.Cqrs.Events;

namespace Klinked.Cqrs.Tests.Fakes.Events
{
    public class FakeRetryEventArgs
    {
        public int TimesExecuted { get; private set; }
        public int TimesToFail { get; }

        public FakeRetryEventArgs(int timesToFail)
        {
            TimesToFail = timesToFail;
        }

        public bool ShouldFail()
        {
            return TimesToFail > TimesExecuted++;
        }
    }
    
    public class FakeRetryEventHandler : IEventHandler<FakeRetryEventArgs>
    {
        public Task Handle(FakeRetryEventArgs args)
        {
            if (args.ShouldFail())
                throw new Exception();
            
            return Task.CompletedTask;
        }
    }
}