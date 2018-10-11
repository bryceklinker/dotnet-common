using System.Threading.Tasks;
using Klinked.Cqrs.Logging.Common;
using Klinked.Cqrs.Queries;
using Microsoft.Extensions.Logging;

namespace Klinked.Cqrs.Logging.Queries
{
    internal class LoggingQueryHandlerDecorator<TArgs, TResult> : IQueryHandler<TArgs, TResult>
    {
        private readonly IQueryHandler<TArgs, TResult> _handler;
        private readonly ILogger _logger;

        public LoggingQueryHandlerDecorator(IQueryHandler<TArgs, TResult> handler, ILoggerFactory loggerFactory)
        {
            _handler = handler;
            _logger = loggerFactory.CreateLogger<LoggingQueryHandlerDecorator<TArgs, TResult>>();
        }

        public async Task<TResult> Execute(TArgs args)
        {
            var name = ArgumentsNameResolver.GetName<TArgs>();
            _logger.LogInformation($"Executing {name} query...");
            var result = await _handler.Execute(args).ConfigureAwait(false);
            _logger.LogInformation($"Executed {name} query.");
            return result;
        }
    }
}