using System;
using Klinked.Cqrs.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Klinked.Cqrs
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKlinkedCqrs(this IServiceCollection services, Action<ICqrsOptionsBuilder> configureBuilder)
        {
            var builder = new CqrsOptionsBuilder();
            configureBuilder(builder);
            return services.AddCqrs(builder.Build());
        }

        public static IServiceCollection AddKlinkedCqrs(this IServiceCollection services, CqrsOptions options)
        {
            services.AddCqrs(options);
            return services;
        }
    }
}