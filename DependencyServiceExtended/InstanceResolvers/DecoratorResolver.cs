using DependencyServiceExtended.Attributes;
using DependencyServiceExtended.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyServiceExtended.InstanceResolvers
{
    internal class DecoratorResolver : IInstanceResolver
    {
        private readonly Type decoratedType;
        private readonly IList<DecoratorImplementationProperties> decorators;

        public DecoratorResolver(Type decoratedType, IList<DecoratorImplementationProperties> decorators)
        {
            this.decoratedType = decoratedType;
            this.decorators = decorators;
        }
        public Func<T> ResolveUsing<T>() where T : class
        {
            return DecoratorFuncResolver<T>;
        }

        private T DecoratorFuncResolver<T>() where T : class
        {
            T instance = (T)Activator.CreateInstance(decoratedType);
            foreach (var decorator in decorators)
            {
                instance = (T)Activator.CreateInstance(decorator.Type, instance);
            }
            return instance;
        }
    }
}
