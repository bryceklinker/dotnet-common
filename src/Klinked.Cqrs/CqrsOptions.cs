using System;
using System.Linq;
using System.Reflection;

namespace Klinked.Cqrs
{
    public class CqrsOptions
    {
        public Assembly[] Assemblies { get; set; }
        public Func<ICqrsBus, ICqrsBus>[] DecoratorFactories { get; set; }
        public Func<IServiceProvider, ICqrsBus, ICqrsBus>[] ProviderDecoratorFactories { get; set; }

        public CqrsOptions(Assembly[] assemblies = null, 
            Func<ICqrsBus, ICqrsBus>[] decoratorFactories = null,
            Func<IServiceProvider, ICqrsBus, ICqrsBus>[] providerDecoratorFactories = null)
        {
            Assemblies = assemblies ?? new Assembly[0];
            DecoratorFactories = decoratorFactories ?? new Func<ICqrsBus, ICqrsBus>[0];
            ProviderDecoratorFactories =
                providerDecoratorFactories ?? new Func<IServiceProvider, ICqrsBus, ICqrsBus>[0];
        }
    }
}