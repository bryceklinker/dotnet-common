using Polly.Retry;

namespace Klinked.Cqrs.Retry.Common
{
    internal interface ICqrsRetryOptions
    {
        RetryPolicy RetryPolicy { get; }
    }

    internal class CqrsRetryOptions : ICqrsRetryOptions
    {
        public RetryPolicy RetryPolicy { get; }

        public CqrsRetryOptions(RetryPolicy retryPolicy)
        {
            RetryPolicy = retryPolicy;
        }
    } 
}