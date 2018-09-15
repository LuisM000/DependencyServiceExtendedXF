using DependencyServiceExtended.Test.Utils;

namespace DependencyServiceExtended.Test.Factories
{
    public static class Containers
    {
        public static Container ContainerWithRegisteredServices()
        {
            var container = new Container();
            container.Register<IService1, Service1>();
            container.Register<IService2, Service2>();
            return container;
        }
    }
}
