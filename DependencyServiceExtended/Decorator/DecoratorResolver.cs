using DependencyServiceExtended.Enums;

namespace DependencyServiceExtended.Decorator
{
    internal class DecoratorResolver
    {
        private readonly Container container;

        private readonly Decorators decorators = new Decorators();

        public DecoratorResolver(Container container)
        {
            this.container = container;
        }

        public void AddDecorator<T, TImpl>() where T : class where TImpl : class//, T
        {
            decorators.Add<T, TImpl>();
        }

        public T DecorateInstance<T>(T instance, DependencyFetchType dependencyFetchType) where T : class
        {
            return decorators.Decorate(container, instance, dependencyFetchType);
        }
    }
}
