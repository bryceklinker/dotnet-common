using System;
using System.Collections.Generic;
using System.Linq;
using Klinked.Cqrs.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs.Events
{
    internal interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler<TArgs>> Create<TArgs>();
    }
    
    internal class EventHandlerFactory : IEventHandlerFactory
    {
        private readonly IServiceProvider _provider;
        private readonly IDecoratorFactory _decoratorFactory;
        private readonly Type[] _decorators;

        public EventHandlerFactory(IServiceProvider provider, Type[] decorators)
        {
            _provider = provider;
            _decorators = decorators;
            _decoratorFactory = new DecoratorFactory(provider);
        }

        public IEnumerable<IEventHandler<TArgs>> Create<TArgs>()
        {
            var eventHandlers = _provider.GetServices<IEventHandler<TArgs>>();
            foreach (var decoratorType in _decorators)
                eventHandlers = eventHandlers.Select(h => _decoratorFactory.CreateHandler(h, decoratorType));
            return eventHandlers;
        }
    }
}