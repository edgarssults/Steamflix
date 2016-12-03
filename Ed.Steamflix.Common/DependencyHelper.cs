using DryIoc;
using System;

namespace Ed.Steamflix.Common
{
    public static class DependencyHelper
    {
        private static IContainer _container;

        /// <summary>
        /// Initialises the container.
        /// </summary>
        /// <param name="container">Container to initialise.</param>
        public static void InitContainer(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Calls a resolve on the container.
        /// </summary>
        /// <typeparam name="T">The type of T to get.</typeparam>
        /// <returns>T or Exception if not wired.</returns>
        public static T Resolve<T>()
        {
            if (_container == null)
            {
                throw new NullReferenceException(nameof(_container));
            }

            return _container.Resolve<T>();
        }
    }
}
