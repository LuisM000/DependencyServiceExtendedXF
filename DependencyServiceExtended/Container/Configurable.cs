using DependencyServiceExtended.Rules;

namespace DependencyServiceExtended
{
    internal class Configurable<T> : IConfigurable<T> where T : class
    {
        private readonly IContainer container;

        public Configurable(IContainer container)
        {
            this.container = container;
        }
        public IConfigurable<T> AddRule(IRule rule)
        {
            container.AddRule<T>(rule);
            return this;
        }

        public IConfigurable<T> AddDecorator<TImp>() where TImp : class, T
        {
            container.AddDecorator<T, TImp>();
            return this;
        }
    }
}
