using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DependencyServiceExtended.Rebindable
{
    internal class RebindableInstances
    {
        private readonly Dictionary<Type, IRebindableInstance<object>> rebindables =
            new Dictionary<Type, IRebindableInstance<object>>();

        public T GetOrCreate<T>() where T : class
        {
            var rebindableInstance = GetRebindableInstance<T>(typeof(T));
            return (T)rebindableInstance.GetInstance(() => DependencyService.Get<T>(DependencyFetchTarget.NewInstance));
        }

        public T GetOrCreate<T>(Type type, Func<T> resolver) where T : class
        {
            var rebindableInstance = GetRebindableInstance<T>(type);
            return (T)rebindableInstance?.GetInstance(resolver);
        }

        public void Rebind()
        {
            foreach (var staticFacade in rebindables)
            {
                staticFacade.Value.Rebind();
            }
        }

        private IRebindableInstance<object> GetRebindableInstance<T>(Type targetType) where T : class
        {
            if (!rebindables.ContainsKey(targetType))
            {
                rebindables.Add(targetType, new RebindableInstance<object>());
            }
            var rebindableInstance = rebindables[targetType];
            return rebindableInstance;
        }

    }
}
