using System.Threading.Tasks;
using Klinked.Cqrs.Events;
using Klinked.Cqrs.Logging.Common;
using Microsoft.Extensions.Logging;

namespace Klinked.Cqrs.Logging.Events
{
    internal class LoggingEventHandlerDecorator<TArgs> : IEventHandler<TArgs>
    {
        private readonly IEventHandler<TArgs> _handler;
        private readonly ILogger _logger;

        public LoggingEventHandlerDecorator(IEventHandler<TArgs> handler, ILoggerFactory loggerFactory)
        {
            _handler = handler;
            _logger = loggerFactory.CreateLogger<IEventHandler<TArgs>>();
        }

        public async Task Handle(TArgs args)
        {
            var eventName = ArgumentsNameResolver.GetName<TArgs>();
            _logger.LogInformation($"Publishing {eventName} event...");
            await _handler.Handle(args).ConfigureAwait(false);
            _logger.LogInformation($"Published {eventName} event.");
        }
    }
}