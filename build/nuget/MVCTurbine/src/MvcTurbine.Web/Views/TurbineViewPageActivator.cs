namespace MvcTurbine.Web.Views {
    using System;
    using System.Web.Mvc;
    using MvcTurbine.ComponentModel;

    /// <summary>
    /// Default implemenation of <see cref="IViewPageActivator"/> for the system to use.
    /// </summary>
	public class TurbineViewPageActivator : IViewPageActivator {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="locator"></param>
		public TurbineViewPageActivator(IServiceLocator locator) {
			ServiceLocator = locator;
		}

        /// <summary>
        /// Gets the associated <see cref="IServiceLocator"/> with this extension point.
        /// </summary>
		public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Creates the specified view type from the container.  If a <see cref="ServiceResolutionException"/> exception is thrown 
        /// from the <see cref="IServiceLocator"/> property, then Activator.CreateInstance is used to create the type.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="type">View type to create.</param>
        /// <returns>Instance of view</returns>
		public object Create(ControllerContext controllerContext, Type type) {
            try {
                return ServiceLocator.Resolve(type);
            }
            catch(ServiceResolutionException) {
                // if the view type wasn't able to be created, then return the default creation.
                return Activator.CreateInstance(type);
            }
		}
	}
}
