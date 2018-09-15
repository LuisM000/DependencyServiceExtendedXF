using System;

namespace DependencyServiceExtended.Rebindable
{
    internal interface IRebindableInstance<T>
    {
        T GetInstance(Func<T> resolver);
        void Rebind();
    }
}
