namespace MvcTurbine.Web.Views {
    using System.Web;
    using System.Web.Mvc;
    using ComponentModel;

    ///<summary>
    /// Extensions for <see cref="ViewContext"/> types.
    ///</summary>
    public static class ViewExtensions {
        /// <summary>
        /// Gets the current <see cref="ITurbineApplication"/> associated with the MVC application.
        /// </summary>
        /// <param name="viewContext">Current view context.</param>
        /// <returns>Current <see cref="ITurbineApplication"/> or null if not applicable.</returns>
        internal static ITurbineApplication TurbineApplication(this ViewContext viewContext) {
            HttpContextBase httpContext = viewContext.HttpContext;
            if (httpContext == null) return null;

            return httpContext.ApplicationInstance as ITurbineApplication;
        }

        /// <summary>
        /// Gets the current <see cref="IRotorContext"/> associated with the MVC application.
        /// </summary>
        /// <param name="controller">Current controller.</param>
        /// <returns>Current <see cref="IRotorContext"/> or null if not applicable.</returns>
        internal static IRotorContext RotorContext(this ViewContext viewContext) {
            ITurbineApplication turbineApplication = TurbineApplication(viewContext);
            return turbineApplication == null ? null : turbineApplication.CurrentContext;
        }

        /// <summary>
        /// Gets the current <see cref="IServiceLocator"/> associated with the MVC application.
        /// </summary>
        /// <param name="viewContext">Current ViewContext.</param>
        /// <returns>Current <see cref="IServiceLocator"/> or null if not applicable</returns>
        public static IServiceLocator ServiceLocator(this ViewContext viewContext) {
            ITurbineApplication turbineApplication = TurbineApplication(viewContext);
            return turbineApplication == null ? null : turbineApplication.ServiceLocator;
        }
    }
}
