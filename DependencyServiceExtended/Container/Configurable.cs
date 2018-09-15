using DependencyServiceExtended.Rules;

namespace DependencyServiceExtended
{
    internal class Configurable<T> : IConfigurable where T : class
    {
        private readonly IContainer container;

        public Configurable(IContainer container)
        {
            this.container = container;
        }
        public IConfigurable AddRule(IRule rule)
        {
            container.AddRule<T>(rule);
            return this;
        }

        public IConfigurable AddDecorator<TImp>() where TImp : class
        {
            container.AddDecorator<T, TImp>();
            return this;
        }
    }
}
