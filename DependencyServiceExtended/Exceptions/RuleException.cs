using System;
using DependencyServiceExtended.Rules;

namespace DependencyServiceExtended.Exceptions
{
    public class RuleException : Exception
    {
        public RuleException(IRule rule) :
            base($"Rule {rule?.GetType()} has thrown an exception")
        {

        }
    }
}
