using DependencyServiceExtended.Attributes;
using DependencyServiceExtended.Test.Utils;

[assembly: DependencyRegister(typeof(IService5), typeof(Service5WithDependencyRegisterAttribute))]
namespace DependencyServiceExtended.Test.Utils
{
    public class Service5WithDependencyRegisterAttribute:IService5
    {
    }
}
