using System;
using System.Collections.Generic;
using DependencyServiceExtended.Enums;

namespace DependencyServiceExtended.Decorator
{
    public class DecoratorsContainer
    {
        private readonly Dictionary<Type, List<Type>> decorators = new Dictionary<Type, List<Type>>();
        private readonly Dictionary<Type, object> globalDecorateInstances = new Dictionary<Type, object>();

        public void Add(Type decoratorType, Type decoratedType)
        {
            if (!decorators.ContainsKey(decoratorType))
            {
                decorators.Add(decoratorType, new List<Type>());
            }

            decorators[decoratorType].Add(decoratedType);
        }

        public void Add<T, TImpl>()
        {
            Add(typeof(T), typeof(TImpl));
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
                        instance = CreateInstance<T>(decoratorType, instance);
                    }
                }
                else if (dependencyFetchType == DependencyFetchType.GlobalInstance)
                {
                    if (!globalDecorateInstances.ContainsKey(TType))
                    {
                        foreach (var decoratorType in decoratorTypes)
                        {
                            instance = CreateInstance<T>(decoratorType, instance);
                        }
                        globalDecorateInstances.Add(TType, instance);
                    }

                    instance = (T)globalDecorateInstances[TType];
                }
                else if (dependencyFetchType == DependencyFetchType.GlobalRebindableInstance)
                {
                    foreach (var decoratorType in decoratorTypes)
                    {
                        instance = container.rebindableInstances.GetOrCreate(decoratorType,
                                        () => CreateInstance<T>(decoratorType, instance));
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
