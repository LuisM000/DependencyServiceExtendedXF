using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Test.Factories;
using DependencyServiceExtended.Test.Utils;
using Xunit;

namespace DependencyServiceExtended.Test
{
    public class DecoratorWithAttributeShould : IClassFixture<XamarinFormsFixture>
    {
        [Fact]
        public void ReturnsTypeOfDecoratorAdded()
        {
            var container = Containers.ContainerWithRegisteredServices();
          
            var service1Decorated = container.Get<IService3>(DependencyFetchType.GlobalInstance);

            Assert.IsType<Service3DecoratorWithAttribute>(service1Decorated);
        }
    }
}
