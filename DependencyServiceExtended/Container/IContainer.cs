using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Rules;

namespace DependencyServiceExtended
{
    public interface IContainer
    {
        IConfigurable Register<T, TImpl>()
            where T : class
            where TImpl : class, T;

        IConfigurable Register<T>() where T : class;

        IConfigurable AddRule<T>(IRule rule) where T : class;

        IConfigurable AddDecorator<T, TImpl>()
            where T : class
            where TImpl : class;//, T;

        T Get<T>(DependencyFetchType dependencyFetchType) where T : class;

        void Rebind();
    }
}
