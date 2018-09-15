using DependencyServiceExtended.Rules;

namespace DependencyServiceExtended
{
    public interface IConfigurable
    {
        IConfigurable AddRule(IRule rule);
        IConfigurable AddDecorator<TImp>() where TImp : class;
    }
}
