using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Test.Factories;
using DependencyServiceExtended.Test.Utils;
using Xunit;

namespace DependencyServiceExtended.Test
{
    public class ContainerShould : IClassFixture<XamarinFormsFixture>
    {
        [Fact]
        public void ReturnsSameInstanceWhenUseGlobalInstance()
        {
            var container = Containers.ContainerWithRegisteredServices();

            var instance1 = container.Get<IService1>(DependencyFetchType.GlobalInstance);
            var instance2 = container.Get<IService1>(DependencyFetchType.GlobalInstance);

            Assert.Equal(instance1, instance2);
        }

        [Fact]
        public void ReturnsDifferentInstanceWhenUseNewInstance()
        {
            var container = Containers.ContainerWithRegisteredServices();

            var instance1 = container.Get<IService1>(DependencyFetchType.NewInstance);
            var instance2 = container.Get<IService1>(DependencyFetchType.NewInstance);

            Assert.NotEqual(instance1, instance2);
        }


        [Fact]
        public void ReturnsSameInstanceWhenUseGlobalRebindableInstance()
        {
            var container = Containers.ContainerWithRegisteredServices();

            var instance1 = container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);
            var instance2 = container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);

            Assert.Equal(instance1, instance2);
        }


        [Fact]
        public void ReturnsDifferentInstancesWithDifferentFetchTypes()
        {
            var container = Containers.ContainerWithRegisteredServices();

            var globalInstance = container.Get<IService1>(DependencyFetchType.GlobalInstance);
            var newInstance = container.Get<IService1>(DependencyFetchType.NewInstance);
            var globalRebindableInstance = container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);

            //Review this assert
            Assert.NotEqual(globalInstance, newInstance);
            Assert.NotEqual(globalInstance, globalRebindableInstance);
            Assert.NotEqual(newInstance, globalRebindableInstance);
        }

    }
}
