using System;
using System.Collections.Generic;
using System.Linq;
using DependencyServiceExtended.Enums;

namespace DependencyServiceExtended.Decorator
{
    public class DecoratorsContainer
    {
        private readonly Dictionary<Type, List<DecoratorImplementationProperties>> decorators = new Dictionary<Type, List<DecoratorImplementationProperties>>();
        private readonly Dictionary<Type, object> globalDecorateInstances = new Dictionary<Type, object>();

        public void Add(Type decoratorType, Type decoratedType, int order)
        {
            if (!decorators.ContainsKey(decoratorType))
            {
                decorators.Add(decoratorType, new List<DecoratorImplementationProperties>());
            }
            decorators[decoratorType].Add(new DecoratorImplementationProperties(decoratedType, order));
            decorators[decoratorType].Sort((x, y) => x.Order.CompareTo(y.Order));
        }

        public void Add<T, TImpl>()
        {
            Add(typeof(T), typeof(TImpl), 1);
        }

        public T Decorate<T>(Container container, T instance, DependencyFetchType dependencyFetchType) where T : class
        {
            Type TType = typeof(T);
            if (decorators.ContainsKey(TType))
            {
                var decoratorTypes = decorators[TType];
                if (dependencyFetchType == DependencyFetchType.NewInstance)
                {
                    foreach (var decoratorType in decoratorTypes)
                    {
                        instance = CreateInstance<T>(decoratorType.Type, instance);
                    }
                }
                else if (dependencyFetchType == DependencyFetchType.GlobalInstance)
                {
                    if (!globalDecorateInstances.ContainsKey(TType))
                    {
                        foreach (var decoratorType in decoratorTypes)
                        {
                            instance = CreateInstance<T>(decoratorType.Type, instance);
                        }
                        globalDecorateInstances.Add(TType, instance);
                    }

                    instance = (T)globalDecorateInstances[TType];
                }
                else if (dependencyFetchType == DependencyFetchType.GlobalRebindableInstance)
                {
                    foreach (var decoratorType in decoratorTypes)
                    {
                        instance = container.rebindableInstances.GetOrCreate(decoratorType.Type,
                                        () => CreateInstance<T>(decoratorType.Type, instance));
                    }
                }
            }
            return instance;
        }

        private T CreateInstance<T>(Type type, params object[] parameters)
        {
            return (T)Activator.CreateInstance(type, parameters);
        }

    }
}
