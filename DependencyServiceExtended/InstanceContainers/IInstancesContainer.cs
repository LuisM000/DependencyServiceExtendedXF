using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyServiceExtended.Decorator;
using DependencyServiceExtended.Enums;
using DependencyServiceExtended.InstanceResolvers;

namespace DependencyServiceExtended.InstanceContainers
{
    internal interface IInstancesContainer
    {
        T GetOrCreate<T>(DependencyFetchType fetchType, IInstanceResolver resolver) where T : class;
        void Rebind();
    }
}
