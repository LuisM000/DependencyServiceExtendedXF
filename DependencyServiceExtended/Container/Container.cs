using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DependencyServiceExtended.Attributes;
using DependencyServiceExtended.Decorator;
using DependencyServiceExtended.Enums;
using DependencyServiceExtended.InstanceContainers;
using DependencyServiceExtended.InstanceResolvers;
using DependencyServiceExtended.Rules;
using Xamarin.Forms;

namespace DependencyServiceExtended
{
    public class Container : IContainer
    {
        private bool hasBeenInitialized;

        private readonly RulesContainer rulesContainer = new RulesContainer();
        private readonly IInstancesContainer instancesContainer = new InstancesContainer();
        private readonly DecoratorsContainer decoratorsContainer = new DecoratorsContainer();
        private readonly Dictionary<Type,Type> typeToImplementationType=new Dictionary<Type, Type>();

        public IConfigurable<T> Register<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            typeToImplementationType.Add(typeof(T),typeof(TImpl));
            return new Configurable<T>(this);
        }

        public IConfigurable<T> Register<T>() where T : class
        {
            typeToImplementationType.Add(typeof(T), typeof(T));
            return new Configurable<T>(this);
        }

        public IConfigurable<T> AddRule<T>(IRule rule) where T : class
        {
            rulesContainer.AddRule<T>(rule);
            return new Configurable<T>(this);
        }

        public IConfigurable<T> AddDecorator<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            decoratorsContainer.Add<T, TImpl>();
            return new Configurable<T>(this);
        }

        public T Get<T>(DependencyFetchType dependencyFetchType) where T : class
        {
            Initialize();

            rulesContainer.ExecuteRules<T>(dependencyFetchType);

            var instanceResolver = GetInstanceResolver<T>();

            T instance = instancesContainer.GetOrCreate<T>(dependencyFetchType, instanceResolver);

            return instance;
        }

        
        public void Rebind()
        {
            instancesContainer.Rebind();
        }


        private void Initialize()
        {
            if(hasBeenInitialized)
                return;

            RegisterDecoratorsFromAssemblies();

            hasBeenInitialized = true;
        }

        private void RegisterDecoratorsFromAssemblies()
        {
            var assemblies = typeof(Device).GetRuntimeMethod("GetAssemblies", new Type[0]).Invoke(null, null) as Assembly[];
            if(assemblies==null)
                return;

            Type targetAttrType = typeof(DependencyDecoratorAttribute);

            foreach (Assembly assembly in assemblies)
            {
                Attribute[] attributes;

                attributes = assembly.GetCustomAttributes(targetAttrType).ToArray();
                if(attributes.Length == 0)
                    continue;

                foreach (DependencyDecoratorAttribute attribute in attributes)
                {
                    decoratorsContainer.Add(attribute.DecoratedType, attribute.DecoratorType, attribute.Order);
                }
            }
        }

        private IInstanceResolver GetInstanceResolver<T>() where T : class
        {
            var implementationType = typeToImplementationType[typeof(T)];
            var decorators = decoratorsContainer.GetDecorators<T>();
            if (decorators != null)
            {
                return new DecoratorResolver(implementationType, decorators);
            }
            return new BasicInstanceResolver(implementationType);
        }

    }
}
