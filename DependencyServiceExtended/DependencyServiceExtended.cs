using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Rules;

namespace DependencyServiceExtended
{
    public static class DependencyServiceExtended
    {
        private static readonly IContainer instance = new Container();

        public static IConfigurable<T> Register<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            return instance.Register<T, TImpl>();
        }

        public static IConfigurable<T> Register<T>() where T : class
        {
            return instance.Register<T>();
        }

        public static T Get<T>(DependencyFetchType dependencyFetchType) where T : class
        {
            return instance.Get<T>(dependencyFetchType);
        }

        public static IConfigurable<T> AddRule<T>(IRule rule) where T : class
        {
            return instance.AddRule<T>(rule);
        }

        public static IConfigurable<T> AddDecorator<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            return instance.AddDecorator<T,TImpl>();
        }

        public static void Rebind()
        {
            instance.Rebind();
        }

    }
}
