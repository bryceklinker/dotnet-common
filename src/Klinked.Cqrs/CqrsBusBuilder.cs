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
        ICqrsBusBuilder UseDecoratorFactory(Func<ICqrsBus, ICqrsBus> decoratorFactory);
        ICqrsBusBuilder UseDecoratorFactory(Func<IServiceProvider, ICqrsBus, ICqrsBus> providerDecoratorFactory);
        ICqrsBusBuilder UseServices(IServiceCollection services);
        ICqrsBus Build();
    }

    internal class CqrsBusBuilder : ICqrsBusBuilder
    {
        private readonly ICqrsOptionsBuilder _optionsBuilder;
        private readonly IServiceCollection _services;
        
        public CqrsBusBuilder(ICqrsOptionsBuilder optionsBuilder)
        {
            _optionsBuilder = optionsBuilder;
            _services = new ServiceCollection();
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

        public ICqrsBusBuilder UseDecoratorFactory(Func<ICqrsBus, ICqrsBus> decoratorFactory)
        {
            _optionsBuilder.UseDecoratorFactory(decoratorFactory);
            return this;
        }

        public ICqrsBusBuilder UseDecoratorFactory(Func<IServiceProvider, ICqrsBus, ICqrsBus> providerDecoratorFactory)
        {
            _optionsBuilder.UseDecoratorFactory(providerDecoratorFactory);
            return this;
        }

        public ICqrsBusBuilder UseServices(IServiceCollection services)
        {
            foreach (var descriptor in services)
                _services.Add(descriptor);
            return this;
        }

        public ICqrsBus Build()
        {
            var options = _optionsBuilder.Build();
            return _services
                .AddCqrs(options)
                .BuildServiceProvider()
                .GetRequiredService<ICqrsBus>();
        }
    }
}