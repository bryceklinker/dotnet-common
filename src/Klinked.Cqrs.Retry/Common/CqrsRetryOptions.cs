using Polly.Retry;

namespace Klinked.Cqrs.Retry.Common
{
    internal interface ICqrsRetryOptions
    {
        AsyncRetryPolicy RetryPolicy { get; }
    }

    internal class CqrsRetryOptions : ICqrsRetryOptions
    {
        public AsyncRetryPolicy RetryPolicy { get; }

        public CqrsRetryOptions(AsyncRetryPolicy retryPolicy)
        {
            RetryPolicy = retryPolicy;
        }
    } 
}