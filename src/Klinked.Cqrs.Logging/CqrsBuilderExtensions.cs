using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Klinked.Cqrs.Logging
{
    public static class CqrsBuilderExtensions
    {
        public static ICqrsOptionsBuilder AddLogging(this ICqrsOptionsBuilder builder)
        {
            return builder.UseDecoratorFactory((p, b) => new LoggingCqrsBus(b, p.GetRequiredService<ILoggerFactory>()));
        }
        
        public static ICqrsBusBuilder AddLogging(this ICqrsBusBuilder builder, ILoggerFactory loggerFactory)
        {
            return builder.UseDecoratorFactory(b => new LoggingCqrsBus(b, loggerFactory));
        }
    }
}