using System;
using Klinked.Cqrs.Common;
using Klinked.Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs.Commands
{
    internal interface ICommandHandlerFactory
    {
        ICommandHandler<TArgs> Create<TArgs>();
    }
    
    internal class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider _provider;
        private readonly IDecoratorFactory _decoratorFactory;
        private readonly Type[] _decorators;

        public CommandHandlerFactory(IServiceProvider provider, Type[] decorators)
        {
            _provider = provider;
            _decorators = decorators;
            _decoratorFactory = new DecoratorFactory(provider);
        }

        public ICommandHandler<TArgs> Create<TArgs>()
        {
            var commandHandler = _provider.GetRequiredService<ICommandHandler<TArgs>>();
            foreach (var decoratorType in _decorators)
                commandHandler = _decoratorFactory.CreateHandler(commandHandler, decoratorType);
            return commandHandler;
        }
    }
}