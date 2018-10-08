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
        private readonly IServiceProvider _provider;

        public KlinkedCqrsBus(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task Execute<TCommandArgs>(TCommandArgs args)
        {
            var handler = _provider.GetRequiredService<ICommandHandler<TCommandArgs>>();
            await handler.Execute(args).ConfigureAwait(false);
        }

        public async Task<TResult> Execute<TArgs, TResult>(TArgs args)
        {
            var handler = _provider.GetRequiredService<IQueryHandler<TArgs, TResult>>();
            return await handler.Execute(args).ConfigureAwait(false);
        }

        public async Task Publish<TArgs>(TArgs args)
        {
            var handlers = _provider.GetServices<IEventHandler<TArgs>>();
            foreach (var handler in handlers)
                await handler.Handle(args);
        }
    }
}