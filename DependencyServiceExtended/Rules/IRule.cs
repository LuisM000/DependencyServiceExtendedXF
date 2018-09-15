using DependencyServiceExtended.Enums;

namespace DependencyServiceExtended.Rules
{
    public interface IRule
    {
        void ExecuteRule(DependencyFetchType dependencyFetchType);
    }
}
