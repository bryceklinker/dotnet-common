using System;
using Klinked.Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs.Common
{
    internal interface IDecoratorFactory
    {
        THandler CreateHandler<THandler>(THandler handler, Type decoratorType);
    }

    internal class DecoratorFactory : IDecoratorFactory
    {
        private readonly IServiceProvider _provider;

        public DecoratorFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public THandler CreateHandler<THandler>(THandler handler, Type decoratorType)
        {
            var concreteDecoratorType = GetConcreteDecoratorType<THandler>(decoratorType);
            return (THandler) ActivatorUtilities.CreateInstance(_provider, concreteDecoratorType, handler);
        }

        private static Type GetConcreteDecoratorType<THandler>(Type decoratorType)
        {
            var handlerTypeParameters = typeof(THandler).GenericTypeArguments;
            return decoratorType.MakeGenericType(handlerTypeParameters);
        }
    }
}