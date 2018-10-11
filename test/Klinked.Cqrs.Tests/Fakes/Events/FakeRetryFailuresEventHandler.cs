using System;
using System.Threading.Tasks;
using Klinked.Cqrs.Events;

namespace Klinked.Cqrs.Tests.Fakes.Events
{
    public class FakeRetryEventArgs
    {
        public int NumberOfTimesToFail { get; }
        public int NumberOfAttemptsToHandle { get; private set; }
        public int NumberOfSuccessfulHandles { get; private set; }

        public FakeRetryEventArgs(int numberOfTimesToFail)
        {
            NumberOfTimesToFail = numberOfTimesToFail;
        }
        
        public void Attempt()
        {
            NumberOfAttemptsToHandle++;
        }
        
        public void Handled()
        {
            NumberOfSuccessfulHandles++;
        }
    }

    public class FakeRetryWorkingEventHandler : IEventHandler<FakeRetryEventArgs>
    {
        public Task Handle(FakeRetryEventArgs args)
        {
            args.Attempt();
            args.Handled();
            return Task.CompletedTask;
        }
    }

    public class FakeRetryFailuresEventHandler : IEventHandler<FakeRetryEventArgs>
    {
        public static int TimesHandled = 0;
        
        public Task Handle(FakeRetryEventArgs args)
        {
            args.Attempt();
            if (args.NumberOfTimesToFail > TimesHandled++)
                throw new Exception();
            
            args.Handled();
            return Task.CompletedTask;
        }
    }
}