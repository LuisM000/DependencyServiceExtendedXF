using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Rules;
using DependencyServiceExtended.Test.Factories;
using DependencyServiceExtended.Test.Utils;
using Moq;
using Xunit;

namespace DependencyServiceExtended.Test
{
    public class RuleShould : IClassFixture<ContainerFixture>
    {
        [Fact]
        public void ExecuteRuleWhenGetsAnInstanceWithRule()
        {
            Mock<IRule> ruleMock = new Mock<IRule>();
            var container = Containers.ContainerWithRegisteredServices();
            container.AddRule<IService1>(ruleMock.Object);

            container.Get<IService1>(DependencyFetchType.NewInstance);

            ruleMock.Verify(r => r.ExecuteRule(It.IsAny<DependencyFetchType>()), Times.Once);
        }


        [Fact]
        public void ExecuteAllRulesWhenGetsAnInstanceWithRules()
        {
            Mock<IRule> ruleMock = new Mock<IRule>();
            var container = Containers.ContainerWithRegisteredServices();
            container.AddRule<IService1>(ruleMock.Object);
            container.AddRule<IService1>(ruleMock.Object);
            container.AddRule<IService1>(ruleMock.Object);

            container.Get<IService1>(DependencyFetchType.NewInstance);

            ruleMock.Verify(r => r.ExecuteRule(It.IsAny<DependencyFetchType>()), Times.Exactly(3));
        }


        [Fact]
        public void NotExecuteRulesForDifferentInstance()
        {
            Mock<IRule> ruleMock = new Mock<IRule>();
            var container = Containers.ContainerWithRegisteredServices();
            container.AddRule<IService1>(ruleMock.Object);

            container.Get<IService2>(DependencyFetchType.NewInstance);

            ruleMock.Verify(r => r.ExecuteRule(It.IsAny<DependencyFetchType>()), Times.Never);
        }
    }
}
