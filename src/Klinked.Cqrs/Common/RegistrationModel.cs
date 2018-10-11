using System;
using Klinked.Cqrs.Commands;
using Klinked.Cqrs.Events;
using Klinked.Cqrs.Queries;

namespace Klinked.Cqrs.Common
{
    internal class RegistrationModel
    {
        public Type InterfaceType { get; }
        public Type ImplementationType { get; }

        public RegistrationModel(Type interfaceType, Type implementationType)
        {
            InterfaceType = interfaceType;
            ImplementationType = implementationType;
        }
    }
}