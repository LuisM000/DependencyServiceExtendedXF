
using System;

namespace DependencyServiceExtended.InstanceResolvers
{
    internal class BasicInstanceResolver : IInstanceResolver
    {
        private readonly Type type;

        public BasicInstanceResolver(Type type)
        {
            this.type = type;
        }
        public Func<T> ResolveUsing<T>() where T : class
        {
            return () => (T)Activator.CreateInstance(type);
        }
    }
}
