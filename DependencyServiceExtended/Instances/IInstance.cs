using System;
using DependencyServiceExtended.InstanceResolvers;

namespace DependencyServiceExtended.Instances
{
    internal interface IInstance<T> where T : class
    {
        T Get(IInstanceResolver resolver);
        void Rebind();
    }
}
