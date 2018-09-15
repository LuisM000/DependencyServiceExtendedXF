using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Exceptions;

namespace DependencyServiceExtended.Rules
{
    public class DependencyFetchRule : IRule
    {
        private readonly DependencyFetchType dependencyFetchType;

        public DependencyFetchRule(DependencyFetchType dependencyFetchType)
        {
            this.dependencyFetchType = dependencyFetchType;
        }

        public void ExecuteRule(DependencyFetchType dependencyFetchType)
        {
            if (dependencyFetchType != this.dependencyFetchType)
                throw new RuleException(this);
        }
    }
}
