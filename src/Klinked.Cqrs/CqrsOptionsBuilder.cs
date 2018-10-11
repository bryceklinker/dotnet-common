using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Klinked.Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs
{
    public interface ICqrsOptionsBuilder
    {
        ICqrsOptionsBuilder UseAssemblyFor<T>();
        ICqrsOptionsBuilder UseAssembly(Assembly assembly);
        ICqrsOptionsBuilder UseQueryDecorator(Type decoratorType);
        ICqrsOptionsBuilder UseCommandDecorator(Type decoratorType);
        ICqrsOptionsBuilder UseEventDecorator(Type decoratorType);
        ICqrsOptionsBuilder AddTransient<TService, TImplementation>()
            where TService : class 
            where TImplementation : class, TService;
        
        ICqrsOptionsBuilder AddSingleton<TService>(TService service)
            where TService : class;
        CqrsOptions Build();
    }

    internal class CqrsOptionsBuilder : ICqrsOptionsBuilder
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>();
        private readonly List<Type> _queryDecorators = new List<Type>();
        private readonly List<Type> _commandDecorators = new List<Type>();
        private readonly List<Type> _eventDecorators = new List<Type>();
        private readonly IServiceCollection _services;

        public CqrsOptionsBuilder(IServiceCollection services = null)
        {
            _services = services ?? new ServiceCollection();
        }

        public ICqrsOptionsBuilder UseAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
            return this;
        }

        public ICqrsOptionsBuilder UseQueryDecorator(Type decoratorType)
        {
            _queryDecorators.Add(decoratorType);
            return this;
        }

        public ICqrsOptionsBuilder UseCommandDecorator(Type decoratorType)
        {
            _commandDecorators.Add(decoratorType);
            return this;
        }

        public ICqrsOptionsBuilder UseEventDecorator(Type decoratorType)
        {
            _eventDecorators.Add(decoratorType);
            return this;
        }

        public ICqrsOptionsBuilder AddTransient<TService, TImplementation>()
            where TService : class 
            where TImplementation : class, TService
        {
            _services.AddTransient<TService, TImplementation>();
            return this;
        }

        public ICqrsOptionsBuilder AddSingleton<TService>(TService instance)
            where TService : class
        {
            _services.AddSingleton(instance);
            return this;
        }

        public CqrsOptions Build()
        {
            return new CqrsOptions(
                _services,
                _assemblies.ToArray(), 
                _queryDecorators.ToArray(), 
                _commandDecorators.ToArray(), 
                _eventDecorators.ToArray()
            );
        }

        public ICqrsOptionsBuilder UseAssemblyFor<T>()
        {
            return UseAssembly(typeof(T).Assembly);
        }
    }
}