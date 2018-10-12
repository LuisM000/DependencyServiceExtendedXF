using System;
using System.Collections.Generic;
using System.Linq;
using DependencyServiceExtended.Enums;
using DependencyServiceExtended.InstanceResolvers;
using DependencyServiceExtended.Instances;

namespace DependencyServiceExtended.InstanceContainers
{
    internal class InstancesContainer : IInstancesContainer
    {
        private readonly IList<Tuple<Type,DependencyFetchType, IInstance<object>>> instances = 
            new List<Tuple<Type, DependencyFetchType, IInstance<object>>>();

        public T GetOrCreate<T>(DependencyFetchType fetchType, IInstanceResolver resolver) where T : class
        {
            var instance = GetInstance(typeof(T), fetchType);
            return (T)instance?.Get(resolver);
        }

        public void Rebind()
        {
            foreach (var instance in instances)
            {
                instance.Item3.Rebind();
            }
        }

        private IInstance<object> GetInstance(Type targetType, DependencyFetchType fetchType)
        {
            var instance = instances.FirstOrDefault(i => i.Item1 == targetType && i.Item2 == fetchType)?.Item3;
            if (instance == null)
            {
                switch (fetchType)
                {
                    case DependencyFetchType.GlobalInstance:
                        instance = new GlobalInstance<object>();
                        break;
                    case DependencyFetchType.GlobalRebindableInstance:
                        instance = new RebindableInstance<object>();
                        break;
                    case DependencyFetchType.NewInstance:
                        instance = new NewInstance<object>();
                        break;
                    default:
                        instance = new GlobalInstance<object>();
                        break;
                }
                instances.Add(new Tuple<Type, DependencyFetchType, IInstance<object>>(targetType,fetchType,instance));
            }
            return instance;
        }
    }
}
