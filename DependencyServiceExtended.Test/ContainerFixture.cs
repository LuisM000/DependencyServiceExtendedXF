using System;

namespace DependencyServiceExtended.Test
{
    public class ContainerFixture
    {
        public ContainerFixture()
        {
            var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            Assemblies.AppDomain.Instance.RegisterAssemblies(domainAssemblies);
        }

    }
}
