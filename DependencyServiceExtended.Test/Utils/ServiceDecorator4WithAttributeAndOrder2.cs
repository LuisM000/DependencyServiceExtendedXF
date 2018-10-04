using DependencyServiceExtended.Attributes;
using DependencyServiceExtended.Test.Utils;

[assembly: DependencyDecorator(typeof(IService4), typeof(ServiceDecorator4WithAttributeAndOrder2), 2)]
namespace DependencyServiceExtended.Test.Utils
{
    public class ServiceDecorator4WithAttributeAndOrder2 : IService4
    {
        public IService4 InnerService { get; }

        public ServiceDecorator4WithAttributeAndOrder2(IService4 innerService)
        {
            this.InnerService = innerService;
        }
    }
}
