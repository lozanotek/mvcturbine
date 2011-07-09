namespace MvcTurbine.Web {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Provides the Dependency Resolver for the MVC3 runtime to use.
    /// </summary>
    public class TurbineDependencyResolver : IDependencyResolver {
        private readonly IServiceLocator serviceLocator;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serviceLocator"></param>
        public TurbineDependencyResolver(IServiceLocator serviceLocator) {
            this.serviceLocator = serviceLocator;
        }

        /// <summary>
        /// Gets the associated <see cref="IServiceLocator"/> with the resolver.
        /// </summary>
        public IServiceLocator ServiceLocator {
            get { return serviceLocator; }
        }

        /// <summary>
        /// Gets the service instance by the specified service type.
        /// </summary>
        /// <param name="serviceType">Service type to search.</param>
        /// <returns></returns>
        public object GetService(Type serviceType) {
            try {
                return serviceLocator.Resolve(serviceType);
            }
            catch {
                return null;
            }
        }

        /// <summary>
        /// Gets the services registered by the specified type.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType) {
            return serviceLocator.ResolveServices(serviceType);
        }
    }
}
