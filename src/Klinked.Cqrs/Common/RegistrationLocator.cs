using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Klinked.Cqrs.Commands;
using Klinked.Cqrs.Events;
using Klinked.Cqrs.Queries;

namespace Klinked.Cqrs.Common
{
    internal class RegistrationLocator
    {
        private static readonly Type CommandHandlerType = typeof(ICommandHandler<>);
        private static readonly Type QueryHandlerType = typeof(IQueryHandler<,>);
        private static readonly Type EventHandlerType = typeof(IEventHandler<>);
        
        public RegistrationModel[] GetRegistrations(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .SelectMany(GetRegistrations)
                .ToArray();
        }

        private static IEnumerable<RegistrationModel> GetRegistrations(Assembly assembly)
        {
            return assembly.GetExportedTypes()
                .Where(ImplementsCqrsInterface)
                .SelectMany(CreateRegistration)
                .ToArray();
        }

        private static IEnumerable<RegistrationModel> CreateRegistration(Type implementation)
        {
            return implementation
                .GetInterfaces()
                .Where(t => t.IsGenericType)
                .Select(t => new
                    {
                        TypeArguments = t.GetGenericArguments(),
                        InterfaceType = GetCqrsInterfaceType(t)
                    })
                .Select(t => new RegistrationModel(t.InterfaceType.MakeGenericType(t.TypeArguments), implementation));
        }

        private static Type GetCqrsInterfaceType(Type type)
        {
            var genericTypeDefinition = type.GetGenericTypeDefinition();
            if (genericTypeDefinition == CommandHandlerType)
                return CommandHandlerType;

            if (genericTypeDefinition == QueryHandlerType)
                return QueryHandlerType;

            return EventHandlerType;
        }
        
        private static bool ImplementsCqrsInterface(Type type)
        {
            return type.ImplementsInterface(CommandHandlerType)
                   || type.ImplementsInterface(QueryHandlerType)
                   || type.ImplementsInterface(EventHandlerType);
        }
    }
}