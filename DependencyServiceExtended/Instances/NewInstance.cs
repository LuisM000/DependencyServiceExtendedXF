using DependencyServiceExtended.InstanceResolvers;

namespace DependencyServiceExtended.Instances
{
    internal class NewInstance<T> : IInstance<T> where T : class
    {
        public T Get(IInstanceResolver resolver)
        {
            return resolver.ResolveUsing<T>().Invoke();
        }

        public void Rebind()
        {
        }
    }
}

