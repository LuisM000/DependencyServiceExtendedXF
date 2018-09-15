using System;
using System.Collections.Generic;
using DependencyServiceExtended.Enums;

namespace DependencyServiceExtended.Rules
{
    public class RulesContainer
    {
        private readonly Dictionary<Type, RuleComposite> rules = new Dictionary<Type, RuleComposite>();

        public void AddRule<T>(IRule rule) where T : class
        {
            Type targetType = typeof(T);
            if (!rules.ContainsKey(targetType))
            {
                rules.Add(targetType, new RuleComposite());
            }
            rules[targetType].AddRule(rule);
        }

        public void ExecuteRules<T>(DependencyFetchType dependencyFetchType)
        {
            Type targetType = typeof(T);
            if (rules.ContainsKey(targetType))
            {
                rules[targetType].ExecuteRule(dependencyFetchType);
            }
        }
    }
}
