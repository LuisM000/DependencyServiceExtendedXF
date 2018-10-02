﻿using System;
using System.Linq;
using System.Reflection;
using DependencyServiceExtended.Attributes;
using DependencyServiceExtended.Decorator;
using DependencyServiceExtended.Enums;
using DependencyServiceExtended.Rebindable;
using DependencyServiceExtended.Rules;
using Xamarin.Forms;

namespace DependencyServiceExtended
{
    public class Container : IContainer
    {
        private bool hasBeenInitialized;

        private readonly RulesContainer rulesContainer = new RulesContainer();
        internal readonly RebindableInstances rebindableInstances = new RebindableInstances();
        private readonly DecoratorResolver decoratorResolver;

        public Container()
        {
            decoratorResolver = new DecoratorResolver(this);
        }

        public IConfigurable<T> Register<T, TImpl>()
            where T : class
            where TImpl : class, T
        {
            DependencyService.Register<T, TImpl>();
            return new Configurable<T>(this);
        }

        public IConfigurable<T> Register<T>() where T : class
        {
            DependencyService.Register<T>();
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
            decoratorResolver.AddDecorator<T, TImpl>();
            return new Configurable<T>(this);
        }

        public T Get<T>(DependencyFetchType dependencyFetchType) where T : class
        {
            Initialize();

            rulesContainer.ExecuteRules<T>(dependencyFetchType);

            T instance = null;
            switch (dependencyFetchType)
            {
                case DependencyFetchType.GlobalInstance:
                    instance = DependencyService.Get<T>(DependencyFetchTarget.GlobalInstance);
                    break;
                case DependencyFetchType.NewInstance:
                    instance = DependencyService.Get<T>(DependencyFetchTarget.NewInstance);
                    break;
                case DependencyFetchType.GlobalRebindableInstance:
                    instance = rebindableInstances.GetOrCreate<T>();
                    break;
            }

            instance = decoratorResolver.DecorateInstance(instance, dependencyFetchType);

            return instance;
        }

        public void Rebind()
        {
            rebindableInstances.Rebind();
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
                    decoratorResolver.AddDecorator(attribute.DecoratedType, attribute.DecoratorType);
                }
            }
        }
    }
}
