using System.Threading.Tasks;
using Klinked.Cqrs.Commands;
using Klinked.Cqrs.Retry.Common;
using Polly.Retry;

namespace Klinked.Cqrs.Retry.Commands
{
    internal class RetryCommandHandlerDecorator<TArgs> : ICommandHandler<TArgs>
    {
        private readonly ICommandHandler<TArgs> _handler;
        private readonly AsyncRetryPolicy _retryPolicy;

        public RetryCommandHandlerDecorator(ICommandHandler<TArgs> handler, ICqrsRetryOptions options)
        {
            _handler = handler;
            _retryPolicy = options.RetryPolicy;
        }

        public async Task ExecuteAsync(TArgs args)
        {
            await _retryPolicy
                .ExecuteAsync(async () => await _handler.ExecuteAsync(args).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}