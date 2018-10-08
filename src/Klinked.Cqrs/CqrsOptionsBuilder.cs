using System;
using System.Collections.Generic;
using System.Reflection;

namespace Klinked.Cqrs
{
    public interface ICqrsOptionsBuilder
    {
        ICqrsOptionsBuilder UseAssemblyFor<T>();
        ICqrsOptionsBuilder UseAssembly(Assembly assembly);
        ICqrsOptionsBuilder UseDecoratorFactory(Func<ICqrsBus, ICqrsBus> decoratorFactory);
        ICqrsOptionsBuilder UseDecoratorFactory(Func<IServiceProvider,ICqrsBus,ICqrsBus> providerDecoratorFactory);
        CqrsOptions Build();
    }

    internal class CqrsOptionsBuilder : ICqrsOptionsBuilder
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>();
        private readonly List<Func<ICqrsBus, ICqrsBus>> _decoratorFactories = new List<Func<ICqrsBus, ICqrsBus>>();
        private readonly List<Func<IServiceProvider, ICqrsBus, ICqrsBus>> _providerDecoratorFactories = new List<Func<IServiceProvider, ICqrsBus, ICqrsBus>>();

        public ICqrsOptionsBuilder UseAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
            return this;
        }

        public ICqrsOptionsBuilder UseDecoratorFactory(Func<ICqrsBus, ICqrsBus> decoratorFactory)
        {
            _decoratorFactories.Add(decoratorFactory);
            return this;
        }

        public ICqrsOptionsBuilder UseDecoratorFactory(Func<IServiceProvider, ICqrsBus, ICqrsBus> providerDecoratorFactory)
        {
            _providerDecoratorFactories.Add(providerDecoratorFactory);
            return this;
        }

        public CqrsOptions Build()
        {
            return new CqrsOptions(_assemblies.ToArray(), _decoratorFactories.ToArray(), _providerDecoratorFactories.ToArray());
        }

        public ICqrsOptionsBuilder UseAssemblyFor<T>()
        {
            return UseAssembly(typeof(T).Assembly);
        }
    }
}