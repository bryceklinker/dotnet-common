using System;
using Klinked.Cqrs.Retry.Commands;
using Klinked.Cqrs.Retry.Common;
using Klinked.Cqrs.Retry.Events;
using Klinked.Cqrs.Retry.Queries;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;

namespace Klinked.Cqrs.Retry
{
    public static class CqrsBuilderExtensions
    {
        private static readonly AsyncRetryPolicy DefaultRetryPolicy = Policy.Handle<Exception>().RetryAsync(3);
        
        public static ICqrsBusBuilder AddRetry(this ICqrsBusBuilder builder)
        {
            return builder.AddRetry(DefaultRetryPolicy);
        }

        public static ICqrsOptionsBuilder AddRetry(this ICqrsOptionsBuilder builder)
        {
            return builder.AddRetry(DefaultRetryPolicy);
        }

        public static ICqrsBusBuilder AddRetry(this ICqrsBusBuilder builder, AsyncRetryPolicy policy)
        {
            return builder
                .AddSingleton<ICqrsRetryOptions>(new CqrsRetryOptions(policy))
                .UseCommandDecorator(typeof(RetryCommandHandlerDecorator<>))
                .UseEventDecorator(typeof(RetryEventHandlerDecorator<>))
                .UseQueryDecorator(typeof(RetryQueryHandlerDecorator<,>));
        }

        public static ICqrsOptionsBuilder AddRetry(this ICqrsOptionsBuilder builder, AsyncRetryPolicy policy)
        {
            return builder
                .AddSingleton<ICqrsRetryOptions>(new CqrsRetryOptions(policy))
                .UseCommandDecorator(typeof(RetryCommandHandlerDecorator<>))
                .UseEventDecorator(typeof(RetryEventHandlerDecorator<>))
                .UseQueryDecorator(typeof(RetryQueryHandlerDecorator<,>));
        }
    }
}