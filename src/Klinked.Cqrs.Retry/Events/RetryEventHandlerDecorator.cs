using System.Threading.Tasks;
using Klinked.Cqrs.Events;
using Klinked.Cqrs.Retry.Common;
using Polly.Retry;

namespace Klinked.Cqrs.Retry.Events
{
    internal class RetryEventHandlerDecorator<TArgs> : IEventHandler<TArgs>
    {
        private readonly IEventHandler<TArgs> _handler;
        private readonly RetryPolicy _retryPolicy;

        public RetryEventHandlerDecorator(IEventHandler<TArgs> handler, ICqrsRetryOptions options)
        {
            _handler = handler;
            _retryPolicy = options.RetryPolicy;
        }

        public async  Task Handle(TArgs args)
        {
            await _retryPolicy
                .ExecuteAsync(async () => await _handler.Handle(args).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}