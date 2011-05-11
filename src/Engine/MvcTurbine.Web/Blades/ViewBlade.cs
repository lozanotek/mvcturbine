namespace MvcTurbine.Web.Blades {
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ComponentModel;
    using Controllers;
    using MvcTurbine.Blades;

    ///<summary>
    /// Blade for all View related components
    ///</summary>
    public class ViewBlade : CoreBlade, ISupportAutoRegistration {
        /// <summary>
        /// Initializes the <see cref="ViewEngines.Engines"/> by pulling all associated <seealso cref="IViewEngine"/> instances
        /// in the current application.
        /// </summary>
        /// <param name="context"></param>
        public override void Spin(IRotorContext context) {
            // Get the current IServiceLocator
            var locator = GetServiceLocatorFromContext(context);

            // Clear all ViewEngines
            ViewEngines.Engines.Clear();

            var viewEngines = GetViewEngines(locator);

            // Add any registered ones
            if (viewEngines != null && viewEngines.Count > 0) {
                foreach (var viewEngine in viewEngines) {
                    ViewEngines.Engines.Add(viewEngine);
                }
            }

            // Re-add the WebForms view engine since that's the default one
            ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new WebFormViewEngine());
        }

        /// <summary>
        /// Gets the <see cref="IViewEngine"/> registered with the system.
        /// </summary>
        /// <param name="locator">Instance of <see cref="IServiceLocator"/> to use.</param>
        /// <returns>List of <see cref="IViewEngine"/>, null otherwise.</returns>
        protected virtual IList<IViewEngine> GetViewEngines(IServiceLocator locator) {
            try {
                return locator.ResolveServices<IViewEngine>();
            } catch {
                return null;
            }
        }

        /// <summary>
        /// Provides the auto-registration for <see cref="IViewEngine"/> types.
        /// </summary>
        /// <param name="registrationList"></param>
        public virtual void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(MvcRegistration.RegisterViewEngine());
        }
    }
}