using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs
{
    public class CqrsOptions
    {
        public Type[] QueryDecorators { get; }
        public Type[] CommandDecorators { get; }
        public Type[] EventDecorators { get; }
        public Assembly[] Assemblies { get; set; }
        public IServiceCollection Services { get; }

        public CqrsOptions(IServiceCollection services = null,
            Assembly[] assemblies = null, 
            Type[] queryDecorators = null,
            Type[] commandDecorators = null,
            Type[] eventDecorators = null)
        {
            Services = services ?? new ServiceCollection();
            QueryDecorators = queryDecorators ?? new Type[0];
            CommandDecorators = commandDecorators ?? new Type[0];
            EventDecorators = eventDecorators ?? new Type[0];
            Assemblies = assemblies ?? new Assembly[0];
        }
    }
}