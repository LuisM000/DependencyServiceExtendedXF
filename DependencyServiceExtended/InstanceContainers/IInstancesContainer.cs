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
