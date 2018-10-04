using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyServiceExtended.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class DependencyDecoratorAttribute : Attribute
    {
        internal Type DecoratedType { get; }
        internal Type DecoratorType { get; }
        public int Order { get; }

        public DependencyDecoratorAttribute(Type decoratedType, Type decoratorType)
            :this(decoratedType, decoratorType,1)
        {
        }

        public DependencyDecoratorAttribute(Type decoratedType, Type decoratorType, int order)
        {
            this.DecoratedType = decoratedType;
            this.DecoratorType = decoratorType;
            Order = order;
        }

    }
}
