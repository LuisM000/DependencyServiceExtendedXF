using System;
using DependencyServiceExtended.Attributes;
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

        public void AddDecorator(DependencyDecoratorAttribute decoratorAttribute)
        {
            decoratorsContainer.Add(decoratorAttribute.DecoratedType,
                            decoratorAttribute.DecoratorType, decoratorAttribute.Order);
        }


        public T DecorateInstance<T>(T instance, DependencyFetchType dependencyFetchType) where T : class
        {
            return decoratorsContainer.Decorate(container, instance, dependencyFetchType);
        }
    }
}
