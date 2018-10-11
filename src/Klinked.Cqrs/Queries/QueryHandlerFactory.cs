using System;
using Klinked.Cqrs.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs.Queries
{
    internal interface IQueryHandlerFactory
    {
        IQueryHandler<TArgs, TResult> Create<TArgs, TResult>();
    }

    internal class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IServiceProvider _provider;
        private readonly DecoratorFactory _decoratorFactory;
        private readonly Type[] _decorators;

        public QueryHandlerFactory(IServiceProvider provider, Type[] decorators)
        {
            _provider = provider;
            _decorators = decorators;
            _decoratorFactory = new DecoratorFactory(provider);
        }

        public IQueryHandler<TArgs, TResult> Create<TArgs, TResult>()
        {
            var queryHandler = _provider.GetRequiredService<IQueryHandler<TArgs, TResult>>();
            foreach (var decoratorType in _decorators)
                queryHandler = _decoratorFactory.CreateHandler(queryHandler, decoratorType);
            return queryHandler;
        }
    }
}