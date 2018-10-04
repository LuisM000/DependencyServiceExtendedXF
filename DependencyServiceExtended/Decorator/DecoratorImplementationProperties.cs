using System;

namespace DependencyServiceExtended.Decorator
{
    internal class DecoratorImplementationProperties
    {
        public Type Type { get; }
        public int Order { get; }

        public DecoratorImplementationProperties(Type type, int order)
        {
            Type = type;
            Order = order;
        }
    }
}