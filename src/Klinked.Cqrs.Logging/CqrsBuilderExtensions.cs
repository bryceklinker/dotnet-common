using Klinked.Cqrs.Logging.Commands;
using Klinked.Cqrs.Logging.Events;
using Klinked.Cqrs.Logging.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Klinked.Cqrs.Logging
{
    public static class CqrsBuilderExtensions
    {
        public static ICqrsOptionsBuilder AddLogging(this ICqrsOptionsBuilder builder)
        {
            return builder
                    .UseQueryDecorator(typeof(LoggingQueryHandlerDecorator<,>))
                    .UseCommandDecorator(typeof(LoggingCommandHandlerDecorator<>))
                    .UseEventDecorator(typeof(LoggingEventHandlerDecorator<>));
        }
        
        public static ICqrsBusBuilder AddLogging(this ICqrsBusBuilder builder, ILoggerFactory loggerFactory)
        {
            return builder
                .AddSingleton(loggerFactory)
                .UseQueryDecorator(typeof(LoggingQueryHandlerDecorator<,>))
                .UseCommandDecorator(typeof(LoggingCommandHandlerDecorator<>))
                .UseEventDecorator(typeof(LoggingEventHandlerDecorator<>));
        }
    }
}