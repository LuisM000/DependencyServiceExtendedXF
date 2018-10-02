

using DependencyServiceExtended.Attributes;
using DependencyServiceExtended.Test.Utils;

[assembly: DependencyDecorator(typeof(IService3), typeof(Service3DecoratorWithAttribute))]
namespace DependencyServiceExtended.Test.Utils
{
    public class Service3DecoratorWithAttribute : IService3
    {
        public IService3 InnerService { get; }

        public Service3DecoratorWithAttribute(IService3 innerService)
        {
            this.InnerService = innerService;
        }
    }
}
