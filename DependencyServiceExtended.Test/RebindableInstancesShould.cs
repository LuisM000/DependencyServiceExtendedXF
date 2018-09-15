using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Test.Factories;
using DependencyServiceExtended.Test.Utils;
using Xunit;

namespace DependencyServiceExtended.Test
{
    public class RebindableInstancesShould : IClassFixture<XamarinFormsFixture>
    {
        [Fact]
        public void ReturnsSameInstance()
        {
            var container = Containers.ContainerWithRegisteredServices();

            var instance1 = container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);
            var instance2 = container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);

            Assert.Equal(instance1, instance2);
        }


        [Fact]
        public void ReturnsDifferentInstanceWhenRebinds()
        {
            var container = Containers.ContainerWithRegisteredServices();

            var instance1 = container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);
            container.Rebind();
            var instance2 = container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);

            Assert.NotEqual(instance1, instance2);
        }
    }
}
