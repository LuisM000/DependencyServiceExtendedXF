using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyServiceExtended.InstanceResolvers
{
    internal interface IInstanceResolver
    {
        Func<T> ResolveUsing<T>() where T: class;
    }
}
