using System;

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