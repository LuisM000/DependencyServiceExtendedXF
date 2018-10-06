using System;
using DependencyServiceExtended.InstanceResolvers;

namespace DependencyServiceExtended.Instances
{
    internal class RebindableInstance<T> : IInstance<T> where T : class
    {
        private T instance;

        public T Get(IInstanceResolver resolver)
        {
            if (instance == null)
                instance = resolver.ResolveUsing<T>().Invoke();
            return instance;
        }

        public void Rebind()
        {
            instance = null;
        }
    }
}
