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
        private readonly Type originType;
        private readonly IList<DecoratorImplementationProperties> decorators;

        public DecoratorResolver(Type originType, IList<DecoratorImplementationProperties> decorators)
        {
            this.originType = originType;
            this.decorators = decorators;
        }
        public Func<T> ResolveUsing<T>() where T : class
        {
            return () =>
            {
                return DecoratorFuncResolver<T>();
            };
        }

        private T DecoratorFuncResolver<T>() where T : class
        {
            T instance = (T)Activator.CreateInstance(originType);
            Type TType = typeof(T);
            foreach (var decorator in decorators)
            {
                instance = CreateInstance<T>(decorator.Type, instance);
            }
            return instance;
        }


        private T CreateInstance<T>(Type type, params object[] parameters)
        {
            return (T)Activator.CreateInstance(type, parameters);
        }
    }
}
