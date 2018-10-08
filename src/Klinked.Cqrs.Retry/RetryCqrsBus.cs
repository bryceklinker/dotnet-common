using System;
using System.Threading.Tasks;
using Polly.Retry;

namespace Klinked.Cqrs.Retry
{
    internal class RetryCqrsBus : ICqrsBus
    {
        private readonly ICqrsBus _cqrsBus;
        private readonly RetryPolicy _retry;

        public RetryCqrsBus(ICqrsBus cqrsBus, RetryPolicy retry)
        {
            _cqrsBus = cqrsBus;
            _retry = retry;
        }

        public async Task Execute<TCommandArgs>(TCommandArgs args)
        {
            await _retry
                .ExecuteAsync(async () => await _cqrsBus.Execute(args).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task<TResult> Execute<TArgs, TResult>(TArgs args)
        {
            return await _retry
                .ExecuteAsync(async () => await _cqrsBus.Execute<TArgs, TResult>(args).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task Publish<TArgs>(TArgs args)
        {
            await _retry
                .ExecuteAsync(async () => await _cqrsBus.Publish(args).ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}