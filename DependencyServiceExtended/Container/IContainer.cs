using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Rules;

namespace DependencyServiceExtended
{
    public interface IContainer
    {
        IConfigurable<T> Register<T, TImpl>()
            where T : class
            where TImpl : class, T;

        IConfigurable<T> Register<T>() where T : class;

        IConfigurable<T> AddRule<T>(IRule rule) where T : class;

        IConfigurable<T> AddDecorator<T, TImpl>()
            where T : class
            where TImpl : class, T;

        T Get<T>(DependencyFetchType dependencyFetchType) where T : class;

        void Rebind();
    }
}
