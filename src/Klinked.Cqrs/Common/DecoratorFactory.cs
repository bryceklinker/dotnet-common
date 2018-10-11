using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs.Common
{
    internal class DecoratorFactory
    {
        private readonly IServiceProvider _provider;

        public DecoratorFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public THandler CreateHandler<THandler>(THandler handler, Type decoratorType)
        {
            var constructor = GetConstructor<THandler>(decoratorType);
            var parameters = GetParameters(constructor, handler);
            return (THandler) constructor.Invoke(parameters);
        }

        private static ConstructorInfo GetConstructor<THandler>(Type decoratorType)
        {
            var handlerTypeParameters = typeof(THandler).GenericTypeArguments;
            return decoratorType.MakeGenericType(handlerTypeParameters)
                .GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .First();
        }

        private object[] GetParameters<THandler>(MethodBase constructor, THandler handler)
        {
            var parameters = constructor.GetParameters();
            return parameters
                .Select(p => GetParameterValue(p.ParameterType, handler))
                .ToArray();
        }

        private object GetParameterValue<THandler>(Type argParameterType, THandler handler)
        {
            return argParameterType.IsGenericType && argParameterType.GetGenericTypeDefinition() == typeof(THandler)
                ? handler
                : _provider.GetRequiredService(argParameterType);
        }
    }
}