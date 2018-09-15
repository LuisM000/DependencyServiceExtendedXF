using System;
using Xamarin.Forms;

namespace DependencyServiceExtended.Rebindable
{
    internal class RebindableInstance<T> : IRebindableInstance<T> where T : class
    {
        private T instance;

        public T GetInstance(Func<T> resolver)
        {
            if (instance == null)
                instance = resolver?.Invoke() ?? DependencyService.Get<T>(DependencyFetchTarget.NewInstance);
            return instance;
        }

        public void Rebind()
        {
            instance = null;
        }
    }
}
