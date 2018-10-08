using System;
using Polly;
using Polly.Retry;

namespace Klinked.Cqrs.Retry
{
    public static class CqrsBuilderExtensions
    {
        private static readonly RetryPolicy DefaultRetryPolicy = Policy.Handle<Exception>().RetryAsync(3);
        
        public static ICqrsBusBuilder AddRetry(this ICqrsBusBuilder builder)
        {
            return builder.AddRetry(DefaultRetryPolicy);
        }

        public static ICqrsBusBuilder AddRetry(this ICqrsBusBuilder builder, RetryPolicy policy)
        {
            return builder.UseDecoratorFactory(bus => new RetryCqrsBus(bus, policy));
        }

        public static ICqrsOptionsBuilder AddRetry(this ICqrsOptionsBuilder builder)
        {
            return builder.AddRetry(DefaultRetryPolicy);
        }
        
        public static ICqrsOptionsBuilder AddRetry(this ICqrsOptionsBuilder builder, RetryPolicy policy)
        {
            return builder.UseDecoratorFactory(bus => new RetryCqrsBus(bus, policy));
        }
    }
}