using DependencyServiceExtended.Rules;

namespace DependencyServiceExtended
{
    public interface IConfigurable<T>
    {
        IConfigurable<T> AddRule(IRule rule);
        IConfigurable<T> AddDecorator<TImp>() where TImp : class, T;
    }
}
