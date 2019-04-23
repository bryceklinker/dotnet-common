using System.Threading.Tasks;
using Klinked.Cqrs.Events;

namespace Klinked.Cqrs.Tests.Fakes.Events
{
    public class FakeEventArgs
    {
        public int TimesHandled { get; set; }
    }
    
    public class FakeEventHandlerOne : IEventHandler<FakeEventArgs>
    {
        public Task HandleAsync(FakeEventArgs args)
        {
            args.TimesHandled++;
            return Task.CompletedTask;
        }
    }
    
    public class FakeEventHandlerTwo : IEventHandler<FakeEventArgs>
    {
        public Task HandleAsync(FakeEventArgs args)
        {
            args.TimesHandled++;
            return Task.CompletedTask;
        }
    }
}