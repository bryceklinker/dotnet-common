using System;
using System.Threading.Tasks;
using Klinked.Cqrs.Commands;
using Klinked.Cqrs.Events;
using Klinked.Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs
{
    internal class KlinkedCqrsBus : ICqrsBus
    {
        private readonly IQueryHandlerFactory _queryHandlerFactory;
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly IEventHandlerFactory _eventHandlerFactory;
        
        public KlinkedCqrsBus(IServiceProvider provider, CqrsOptions options)
        {
            _queryHandlerFactory = new QueryHandlerFactory(provider, options.QueryDecorators);
            _commandHandlerFactory = new CommandHandlerFactory(provider, options.CommandDecorators);
            _eventHandlerFactory = new EventHandlerFactory(provider, options.EventDecorators);
        }

        public async Task ExecuteAsync<TCommandArgs>(TCommandArgs args)
        {
            var handler = _commandHandlerFactory.Create<TCommandArgs>();
            await handler.ExecuteAsync(args).ConfigureAwait(false);
        }

        public async Task<TResult> ExecuteAsync<TArgs, TResult>(TArgs args)
        {
            var handler = _queryHandlerFactory.Create<TArgs, TResult>();
            return await handler.ExecuteAsync(args).ConfigureAwait(false);
        }

        public async Task PublishAsync<TArgs>(TArgs args)
        {
            var handlers = _eventHandlerFactory.Create<TArgs>();
            foreach (var handler in handlers)
                await handler.HandleAsync(args).ConfigureAwait(false);
        }
    }
}