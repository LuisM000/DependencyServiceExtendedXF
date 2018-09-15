namespace DependencyServiceExtended.Test.Utils
{
    public class OtherService1Decorator : IService1
    {
        public IService1 InnerService { get; }

        public OtherService1Decorator(IService1 innerService)
        {
            this.InnerService = innerService;
        }
    }
}
