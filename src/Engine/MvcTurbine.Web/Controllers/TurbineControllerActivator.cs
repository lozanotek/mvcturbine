namespace MvcTurbine.Web.Controllers {
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using ComponentModel;
    using MvcTurbine.Web.Blades;

    /// <summary>
    /// Activator for the MVC3 runtime to spin up controllers and the registered <see cref="IActionInvoker"/>.
    /// </summary>
    public class TurbineControllerActivator : IControllerActivator {
        private static IActionInvoker actionInvoker;
        private static readonly object _lock = new object();

        /// <summary>
        /// Default constructor for the type
        /// </summary>
        /// <param name="serviceLocator"></param>
        public TurbineControllerActivator(IServiceLocator serviceLocator) {
            if (serviceLocator == null) {
                throw new ArgumentNullException("serviceLocator");
            }

            ServiceLocator = serviceLocator;
        }

        /// <summary>
        /// Gets the current instance of <see cref="IServiceLocator"/> for the factory.
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Resolves the controller instance form <see cref="ServiceLocator"/> and assigns the
        /// registered <see cref="IActionInvoker"/> with the system.
        /// </summary>
        /// <param name="requestContext">Current request context.</param>
        /// <param name="controllerType">Current controlle type.</param>
        /// <returns></returns>
        public IController Create(RequestContext requestContext, Type controllerType) {
            var instance = ServiceLocator.Resolve(controllerType);
            var controller = instance as Controller;

            // If you inherit from controller, implement this fine work around
            if (controller != null) {
                controller.ActionInvoker = GetActionInvoker();
            }

            return controller;
        }

        /// <summary>
        /// Gets the registered <see cref="IActionInvoker"/> within the system.
        /// </summary>
        /// <returns></returns>
        protected virtual IActionInvoker GetActionInvoker() {
            if (actionInvoker == null) {
                lock (_lock) {
                    if (actionInvoker == null) {
                        try {
                            actionInvoker = ServiceLocator.Resolve<IActionInvoker>();
                        }
                        catch (ServiceResolutionException) {
                            actionInvoker = new TurbineActionInvoker(InferredActions.Current);
                        }
                    }
                }
            }

            return actionInvoker;
        }
    }
}