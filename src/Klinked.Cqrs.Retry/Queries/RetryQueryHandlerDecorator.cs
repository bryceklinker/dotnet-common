using System.Threading.Tasks;
using Klinked.Cqrs.Queries;
using Klinked.Cqrs.Retry.Common;
using Polly.Retry;

namespace Klinked.Cqrs.Retry.Queries
{
    internal class RetryQueryHandlerDecorator<TArgs, TResult> : IQueryHandler<TArgs, TResult>
    {
        private readonly IQueryHandler<TArgs, TResult> _handler;
        private readonly RetryPolicy _retryPolicy;

        public RetryQueryHandlerDecorator(IQueryHandler<TArgs, TResult> handler, ICqrsRetryOptions options)
        {
            _handler = handler;
            _retryPolicy = options.RetryPolicy;
        }

        public async Task<TResult> Execute(TArgs args)
        {
            return await _retryPolicy
                .ExecuteAsync(async () => await _handler.Execute(args).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}