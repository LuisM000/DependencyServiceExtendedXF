using System.Reflection;

namespace DependencyServiceExtended.Assemblies
{
    public class AppDomain
    {
        private static AppDomain instance;
        public static AppDomain Instance
        {
            get { return instance ?? (instance = new AppDomain()); }
        }

        internal Assembly[] Assemblies { get; private set; }

        public void RegisterAssemblies(Assembly[] assemblies)
        {
            this.Assemblies = assemblies;
        }
    }
}
