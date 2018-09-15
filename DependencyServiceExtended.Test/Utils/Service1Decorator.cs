namespace DependencyServiceExtended.Test.Utils
{
    public class Service1Decorator : IService1
    {
        public IService1 InnerService { get; }

        public Service1Decorator(IService1 innerService)
        {
            this.InnerService = innerService;
        }
    }
}
