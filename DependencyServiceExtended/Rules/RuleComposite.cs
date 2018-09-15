using System.Collections.Generic;
using DependencyServiceExtended.Enums;

namespace DependencyServiceExtended.Rules
{
    internal class RuleComposite : IRule
    {
        IList<IRule> rules = new List<IRule>();

        public void ExecuteRule(DependencyFetchType dependencyFetchType)
        {
            foreach (var rule in rules)
            {
                rule.ExecuteRule(dependencyFetchType);
            }
        }

        public void AddRule(IRule rule)
        {
            rules.Add(rule);
        }
    }
}
