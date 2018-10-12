using System;

namespace DependencyServiceExtended.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class DependencyRegisterAttribute:Attribute
    {
        internal Type Type { get; }
        internal Type Implementation { get; }

        public DependencyRegisterAttribute(Type type, Type implementation)
        {
            Type = type;
            Implementation = implementation;
        }
        
    }
}
