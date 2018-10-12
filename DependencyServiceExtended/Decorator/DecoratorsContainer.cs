using System;
using System.Collections.Generic;

namespace DependencyServiceExtended.Decorator
{
    internal class DecoratorsContainer
    {
        internal readonly Dictionary<Type, List<DecoratorImplementationProperties>> decorators = new Dictionary<Type, List<DecoratorImplementationProperties>>();

        public void Add(Type decoratedType, Type decoratorType, int order)
        {
            if (!decorators.ContainsKey(decoratedType))
            {
                decorators.Add(decoratedType, new List<DecoratorImplementationProperties>());
            }
            decorators[decoratedType].Add(new DecoratorImplementationProperties(decoratorType, order));
            decorators[decoratedType].Sort((x, y) => x.Order.CompareTo(y.Order));
        }

        public void Add<T, TImpl>()
        {
            Add(typeof(T), typeof(TImpl), 1);
        }


        public IList<DecoratorImplementationProperties> GetDecorators<T>()
        {
            var type = typeof(T);
            return decorators.ContainsKey(type) ? decorators[type] : null;
        }
    }
}
