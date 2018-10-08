using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Klinked.Cqrs.Logging
{
    internal class LoggingCqrsBus : ICqrsBus
    {
        private readonly ICqrsBus _cqrsBus;
        private readonly ILogger _logger;

        public LoggingCqrsBus(ICqrsBus cqrsBus, ILoggerFactory factory)
            : this(cqrsBus, factory.CreateLogger<LoggingCqrsBus>())
        {
            
        }

        private LoggingCqrsBus(ICqrsBus cqrsBus, ILogger logger)
        {
            _cqrsBus = cqrsBus;
            _logger = logger;
        }

        public async Task Execute<TCommandArgs>(TCommandArgs args)
        {
            var commandName = GetArgsName(args); 
            _logger.LogInformation($"Executing {commandName} command...");
            await _cqrsBus.Execute(args).ConfigureAwait(false);
            _logger.LogInformation($"Executed {commandName} command.");
        }

        public async Task<TResult> Execute<TArgs, TResult>(TArgs args)
        {
            var queryName = GetArgsName(args);
            _logger.LogInformation($"Executing {queryName} query...");
            var result = await _cqrsBus.Execute<TArgs, TResult>(args).ConfigureAwait(false);
            _logger.LogInformation($"Executed {queryName} query.");
            return result;
        }

        public async Task Publish<TArgs>(TArgs args)
        {
            var eventName = GetArgsName(args);
            _logger.LogInformation($"Publishing {eventName} event...");
            await _cqrsBus.Publish(args);
            _logger.LogInformation($"Published {eventName} event.");
        }

        private static string GetArgsName<TArgs>(TArgs args)
        {
            return args.GetType().Name;
        }
    }
}