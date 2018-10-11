using System;
using System.Reflection;
using Klinked.Cqrs.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs
{
    public interface ICqrsBusBuilder
    {
        ICqrsBusBuilder UseAssemblyFor<T>();
        ICqrsBusBuilder UseAssembly(Assembly assembly);
        ICqrsBusBuilder UseQueryDecorator(Type decoratorType);
        ICqrsBusBuilder UseCommandDecorator(Type decoratorType);
        ICqrsBusBuilder UseEventDecorator(Type decoratorType);
        ICqrsBusBuilder AddTransient<TService, TImplementation>()
            where TService : class 
            where TImplementation : class, TService;
        ICqrsBusBuilder AddSingleton<TService>(TService instance)
            where TService : class ;
        ICqrsBus Build();
    }

    internal class CqrsBusBuilder : ICqrsBusBuilder
    {
        private readonly ICqrsOptionsBuilder _optionsBuilder;
        
        public CqrsBusBuilder(ICqrsOptionsBuilder optionsBuilder)
        {
            _optionsBuilder = optionsBuilder;
        }

        public ICqrsBusBuilder UseAssemblyFor<T>()
        {
            _optionsBuilder.UseAssemblyFor<T>();
            return this;
        }

        public ICqrsBusBuilder UseAssembly(Assembly assembly)
        {
            _optionsBuilder.UseAssembly(assembly);
            return this;
        }

        public ICqrsBusBuilder UseQueryDecorator(Type decoratorType)
        {
            _optionsBuilder.UseQueryDecorator(decoratorType);
            return this;
        }

        public ICqrsBusBuilder UseCommandDecorator(Type decoratorType)
        {
            _optionsBuilder.UseCommandDecorator(decoratorType);
            return this;
        }

        public ICqrsBusBuilder UseEventDecorator(Type decoratorType)
        {
            _optionsBuilder.UseEventDecorator(decoratorType);
            return this;
        }

        public ICqrsBusBuilder AddTransient<TService, TImplementation>()
            where TService : class 
            where TImplementation : class, TService
        {
            _optionsBuilder.AddTransient<TService, TImplementation>();
            return this;
        }

        public ICqrsBusBuilder AddSingleton<TService>(TService instance)
            where TService : class
        {
            _optionsBuilder.AddSingleton(instance);
            return this;
        }

        public ICqrsBus Build()
        {
            var options = _optionsBuilder.Build();
            return options.Services
                .AddCqrs(options)
                .BuildServiceProvider()
                .GetRequiredService<ICqrsBus>();
        }
    }
}