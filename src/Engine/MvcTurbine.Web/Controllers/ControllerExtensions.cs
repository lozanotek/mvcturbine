namespace MvcTurbine.Web.Controllers {
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Extension methods for Controllers.
    /// </summary>
    public static class ControllerExtensions {
        /// <summary>
        /// Gets the current <see cref="ITurbineApplication"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="ITurbineApplication"/> or null if not applicable.</returns>
        internal static ITurbineApplication TurbineApplication(this ControllerBase controller) {
            var httpContext = controller.ControllerContext.HttpContext;
            if(httpContext == null) return null;

            return httpContext.ApplicationInstance as ITurbineApplication; 
        }

        /// <summary>
        /// Gets the current <see cref="IRotorContext"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="IRotorContext"/> or null if not applicable.</returns>
        internal static IRotorContext RotorContext(this ControllerBase controller) {
            var turbineApplication = TurbineApplication(controller);
            return turbineApplication == null ? null : turbineApplication.CurrentContext;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceLocator"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="IServiceLocator"/> or nul if not applicable</returns>
        public static IServiceLocator ServiceLocator(this ControllerBase controller) {
            var turbineApplication = TurbineApplication(controller);
            return turbineApplication == null ? null : turbineApplication.ServiceLocator;
        }

        /// <summary>
        /// Gets the current <see cref="ITurbineApplication"/> associated with the MVC application.
        /// </summary>
        /// <param name="controllerContext">Current controller.</param>
        /// <returns>Current <see cref="ITurbineApplication"/> or null if not applicable.</returns>
        internal static ITurbineApplication TurbineApplication(this ControllerContext controllerContext)
        {
            var httpContext = controllerContext.HttpContext;
            if (httpContext == null) return null;

            return httpContext.ApplicationInstance as ITurbineApplication;
        }

        /// <summary>
        /// Gets the current <see cref="IRotorContext"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="IRotorContext"/> or null if not applicable.</returns>
        internal static IRotorContext RotorContext(this ControllerContext controller)
        {
            var turbineApplication = TurbineApplication(controller);
            return turbineApplication == null ? null : turbineApplication.CurrentContext;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceLocator"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="IServiceLocator"/> or nul if not applicable</returns>
        public static IServiceLocator ServiceLocator(this ControllerContext controller)
        {
            var turbineApplication = TurbineApplication(controller);
            return turbineApplication == null ? null : turbineApplication.ServiceLocator;
        }
    }
}
