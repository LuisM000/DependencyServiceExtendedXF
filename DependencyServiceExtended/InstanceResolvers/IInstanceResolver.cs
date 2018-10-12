using System;

namespace DependencyServiceExtended.InstanceResolvers
{
    internal interface IInstanceResolver
    {
        Func<T> ResolveUsing<T>() where T: class;
    }
}
