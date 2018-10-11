using System.Threading.Tasks;
using Klinked.Cqrs.Commands;
using Klinked.Cqrs.Logging.Common;
using Microsoft.Extensions.Logging;

namespace Klinked.Cqrs.Logging.Commands
{
    internal class LoggingCommandHandlerDecorator<TArgs> : ICommandHandler<TArgs>
    {
        private readonly ICommandHandler<TArgs> _handler;
        private readonly ILogger _logger;

        public LoggingCommandHandlerDecorator(ICommandHandler<TArgs> handler, ILoggerFactory loggerFactory)
        {
            _handler = handler;
            _logger = loggerFactory.CreateLogger<LoggingCommandHandlerDecorator<TArgs>>();
        }

        public async Task Execute(TArgs args)
        {
            var commandName = ArgumentsNameResolver.GetName<TArgs>();
            _logger.LogInformation($"Executing {commandName} command...");
            await _handler.Execute(args).ConfigureAwait(false);
            _logger.LogInformation($"Executed {commandName} command.");
        }
    }
}