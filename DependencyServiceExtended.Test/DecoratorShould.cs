using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Test.Factories;
using DependencyServiceExtended.Test.Utils;
using Xunit;

namespace DependencyServiceExtended.Test
{
    public class DecoratorShould : IClassFixture<XamarinFormsFixture>
    {
        [Fact]
        public void ReturnsTypeOfLastDecoratorAdded()
        {
            var container = Containers.ContainerWithRegisteredServices();
            container.AddDecorator<IService1, Service1Decorator>();
            container.AddDecorator<IService1, OtherService1Decorator>();

            var service1Decorated = container.Get<IService1>(DependencyFetchType.GlobalInstance);

            Assert.IsType<OtherService1Decorator>(service1Decorated);
        }


        [Fact]
        public void CreatesDecoratedInstancesWithCorrectInnerType()
        {
            var container = Containers.ContainerWithRegisteredServices();
            container.AddDecorator<IService1, Service1Decorator>();
            container.AddDecorator<IService1, OtherService1Decorator>();

            OtherService1Decorator service1Decorated = (OtherService1Decorator)container.Get<IService1>(DependencyFetchType.GlobalInstance);

            Assert.IsType<Service1Decorator>(service1Decorated.InnerService);
            Assert.IsType<Service1>(((Service1Decorator)(service1Decorated.InnerService)).InnerService);
        }

        [Fact]
        public void ReturnsDifferentInstancesWithNewInstanceFetchType()
        {
            var container = Containers.ContainerWithRegisteredServices();
            container.AddDecorator<IService1, Service1Decorator>();
            container.AddDecorator<IService1, OtherService1Decorator>();

            OtherService1Decorator otherService1Decorator1 = (OtherService1Decorator)container.Get<IService1>(DependencyFetchType.NewInstance);
            OtherService1Decorator otherService1Decorator2 = (OtherService1Decorator)container.Get<IService1>(DependencyFetchType.NewInstance);
            Service1Decorator service1Decorator1 = (Service1Decorator)otherService1Decorator1.InnerService;
            Service1Decorator service1Decorator2 = (Service1Decorator)otherService1Decorator2.InnerService;
            IService1 service1 = service1Decorator1.InnerService;
            IService1 service2 = service1Decorator2.InnerService;

            Assert.NotEqual(otherService1Decorator1, otherService1Decorator2);
            Assert.NotEqual(service1Decorator1, service1Decorator2);
            Assert.NotEqual(service1, service2);
        }

        [Fact]
        public void ReturnsSameInstancesWithGlobalInstanceFetchType()
        {
            var container = Containers.ContainerWithRegisteredServices();
            container.AddDecorator<IService1, Service1Decorator>();
            container.AddDecorator<IService1, OtherService1Decorator>();

            OtherService1Decorator otherService1Decorator1 = (OtherService1Decorator)container.Get<IService1>(DependencyFetchType.GlobalInstance);
            OtherService1Decorator otherService1Decorator2 = (OtherService1Decorator)container.Get<IService1>(DependencyFetchType.GlobalInstance);
            Service1Decorator service1Decorator1 = (Service1Decorator)otherService1Decorator1.InnerService;
            Service1Decorator service1Decorator2 = (Service1Decorator)otherService1Decorator2.InnerService;
            IService1 service1 = service1Decorator1.InnerService;
            IService1 service2 = service1Decorator2.InnerService;

            Assert.Equal(otherService1Decorator1, otherService1Decorator2);
            Assert.Equal(service1Decorator1, service1Decorator2);
            Assert.Equal(service1, service2);
        }

        [Fact]
        public void ReturnsSameInstancesWithGlobalRebindableInstanceFetchType()
        {
            var container = Containers.ContainerWithRegisteredServices();
            container.AddDecorator<IService1, Service1Decorator>();
            container.AddDecorator<IService1, OtherService1Decorator>();

            OtherService1Decorator otherService1Decorator1 = (OtherService1Decorator)container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);
            OtherService1Decorator otherService1Decorator2 = (OtherService1Decorator)container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);
            Service1Decorator service1Decorator1 = (Service1Decorator)otherService1Decorator1.InnerService;
            Service1Decorator service1Decorator2 = (Service1Decorator)otherService1Decorator2.InnerService;
            IService1 service1 = service1Decorator1.InnerService;
            IService1 service2 = service1Decorator2.InnerService;

            Assert.Equal(otherService1Decorator1, otherService1Decorator2);
            Assert.Equal(service1Decorator1, service1Decorator2);
            Assert.Equal(service1, service2);
        }

        [Fact]
        public void ReturnsDifferentInstancesWithGlobalRebindableInstanceFetchTypeWhenRebinds()
        {
            var container = Containers.ContainerWithRegisteredServices();
            container.AddDecorator<IService1, Service1Decorator>();
            container.AddDecorator<IService1, OtherService1Decorator>();

            OtherService1Decorator otherService1Decorator1 = (OtherService1Decorator)container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);
            Service1Decorator service1Decorator1 = (Service1Decorator)otherService1Decorator1.InnerService;
            IService1 service1 = service1Decorator1.InnerService;
            container.Rebind();
            OtherService1Decorator otherService1Decorator2 = (OtherService1Decorator)container.Get<IService1>(DependencyFetchType.GlobalRebindableInstance);
            Service1Decorator service1Decorator2 = (Service1Decorator)otherService1Decorator2.InnerService;
            IService1 service2 = service1Decorator2.InnerService;

            Assert.NotEqual(otherService1Decorator1, otherService1Decorator2);
            Assert.NotEqual(service1Decorator1, service1Decorator2);
            Assert.NotEqual(service1, service2);
        }
    }

}
