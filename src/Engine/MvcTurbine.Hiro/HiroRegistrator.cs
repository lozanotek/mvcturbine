using System;
using Hiro;

namespace MvcTurbine.Hiro {
    using MvcTurbine.ComponentModel;

    /// <summary>
    /// A <see cref="IServiceRegistration"/> implementation that leverages the <see cref="IUnityContainer"/> from Unity.    
    /// </summary>
    public abstract class HiroRegistrator : IServiceRegistration {
        /// <summary>
        /// See <see cref="IServiceRegistration.Register"/>        
        /// </summary>
        /// <param name="locator"></param>
        public void Register(IServiceLocator locator) {
            Register(locator.GetUnderlyingContainer<DependencyMap>());
        }

        /// <summary>
        /// See <see cref="IServiceRegistration.Register"/>
        /// </summary>
        /// <param name="dependencyMap"></param>
        public abstract void Register(DependencyMap dependencyMap);
    }
}
