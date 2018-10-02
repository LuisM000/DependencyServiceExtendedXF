using System;
using DependencyServiceExtended.Enums;

namespace DependencyServiceExtended.Decorator
{
    internal class DecoratorResolver
    {
        private readonly Container container;

        private readonly DecoratorsContainer decoratorsContainer = new DecoratorsContainer();

        public DecoratorResolver(Container container)
        {
            this.container = container;
        }

        public void AddDecorator<T, TImpl>() where T : class where TImpl : class, T
        {
            decoratorsContainer.Add<T, TImpl>();
        }

        public void AddDecorator(Type decoratedType, Type decoratorType)
        {
            decoratorsContainer.Add(decoratedType, decoratorType);
        }


        public T DecorateInstance<T>(T instance, DependencyFetchType dependencyFetchType) where T : class
        {
            return decoratorsContainer.Decorate(container, instance, dependencyFetchType);
        }
    }
}
