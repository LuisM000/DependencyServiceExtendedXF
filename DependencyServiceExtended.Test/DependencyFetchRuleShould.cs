using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Exceptions;
using DependencyServiceExtended.Rules;
using DependencyServiceExtended.Test.Factories;
using DependencyServiceExtended.Test.Utils;
using Xunit;

namespace DependencyServiceExtended.Test
{
    public class DependencyFetchRuleShould : IClassFixture<XamarinFormsFixture>
    {
        [Fact]
        public void NotThrowsExceptionIfSatisfiesDependencyFetchType()
        {
            var container = Containers.ContainerWithRegisteredServices();
            container.AddRule<IService1>(new DependencyFetchRule(DependencyFetchType.NewInstance));

            container.Get<IService1>(DependencyFetchType.NewInstance);
        }

        [Fact]
        public void ThrowsExceptionWhenNotSatisfiesDependencyFetchType()
        {
            var container = Containers.ContainerWithRegisteredServices();
            container.AddRule<IService1>(new DependencyFetchRule(DependencyFetchType.GlobalInstance));

            Assert.Throws<RuleException>(() =>
            {
                container.Get<IService1>(DependencyFetchType.NewInstance);
            });
        }

        [Fact]
        public void ThrowsExceptionWhenNotSatisfiesSomeDependencyFetchType()
        {
            var container = Containers.ContainerWithRegisteredServices();
            container.AddRule<IService1>(new DependencyFetchRule(DependencyFetchType.GlobalInstance));
            container.AddRule<IService1>(new DependencyFetchRule(DependencyFetchType.GlobalRebindableInstance));

            Assert.Throws<RuleException>(() =>
            {
                container.Get<IService1>(DependencyFetchType.NewInstance);
            });
        }
    }
}
