using Ninject;
using System;

namespace Ed.Steamflix.Common
{
    public static class DependencyHelper
    {
        /// <summary>
        /// The Ninject kernel.
        /// </summary>
        private static IKernel _kernel;

        /// <summary>
        /// Used to set the ninject kernel that is in use.
        /// </summary>
        /// <param name="kernel"></param>
        public static void InitNinjectKernel(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Calls a get on the current Ninject kernel.
        /// </summary>
        /// <typeparam name="T">The type of T to get.</typeparam>
        /// <returns>T or Exception if not wired.</returns>
        public static T Get<T>()
        {
            if (_kernel == null)
            {
                throw new Exception("_kernel is null");
            }

            return _kernel.Get<T>();
        }
    }
}
