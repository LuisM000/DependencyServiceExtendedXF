using System;
using System.Collections.Generic;
using System.Linq;
using DependencyServiceExtended.Enums;

namespace DependencyServiceExtended.Decorator
{
    internal class DecoratorsContainer
    {
        internal readonly Dictionary<Type, List<DecoratorImplementationProperties>> decorators = new Dictionary<Type, List<DecoratorImplementationProperties>>();

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


        public IList<DecoratorImplementationProperties> GetDecorators<T>()
        {
            var type = typeof(T);
            return decorators.ContainsKey(type) ? decorators[type] : null;
        }
    }
}
