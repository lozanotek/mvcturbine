namespace MvcTurbine.Web.Blades {
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ComponentModel;
    using Controllers;
    using MvcTurbine.Web.Views;

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
            var locator = GetServiceLocatorFromContext(context);

            // Get the current IServiceLocator
            ProcessViewEngineProvider(locator);

            // Clear all ViewEngines
            ViewEngines.Engines.Clear();           

            // Re-add the WebForms view engine since that's the default one
            ViewEngines.Engines.Add(new RazorViewEngine());
            ViewEngines.Engines.Add(new WebFormViewEngine());

            // Register the view engines (if any)
            RegisterViewEngine(locator);
        }

        protected virtual void RegisterViewEngine(IServiceLocator locator) {
            try {
                var engineManager = locator.Resolve<IViewEngineManager>();
                if (engineManager == null) return;

                engineManager.RegisterEngines();
            }
            catch { }           
        }

        protected virtual void ProcessViewEngineProvider(IServiceLocator locator) {
            var viewEngineProviders = GetViewEngineProviders(locator);
            if (viewEngineProviders == null || viewEngineProviders.Count == 0) return;

            var viewEngines = GetFilteredList(viewEngineProviders);
            if (viewEngines == null || viewEngines.Count == 0) return;

            using (locator.Batch()) {
                foreach (var engine in viewEngines) {
                    locator.Register(typeof(IViewEngine), engine.Type, engine.Name);
                }
            }
        }

        /// <summary>
        /// Filters the list of <see cref="ViewEngine"/> from those that have been added/removed from the system 
        /// across all <see cref="IViewEngineProvider"/> with the system.
        /// </summary>
        /// <param name="engineProviders"></param>
        /// <returns></returns>
        protected virtual IList<ViewEngine> GetFilteredList(IList<IViewEngineProvider> engineProviders) {
            var allInclude = new List<ViewEngine>();
            var allExclude = new List<ViewEngine>();

            foreach (var provider in engineProviders) {
                var registrations = provider.GetViewEngineRegistrations();

                if (registrations == null || registrations.Count == 0) continue;

                var includeList = registrations
                    .Where(reg => !reg.IsRemoved)
                    .ToList();

                var excludeList = registrations
                    .Where(reg => reg.IsRemoved)
                    .ToList();

                allInclude.AddRange(includeList);
                allExclude.AddRange(excludeList);
            }

            // Get the distinct modules based on the name
            allExclude = allExclude.Distinct().ToList();
            allInclude = allInclude.Distinct().ToList();

            return allInclude.Except(allExclude).ToList();
        }

        /// <summary>
        /// Gets the <see cref="IViewEngine"/> registered with the system.
        /// </summary>
        /// <param name="locator">Instance of <see cref="IServiceLocator"/> to use.</param>
        /// <returns>List of <see cref="IViewEngine"/>, null otherwise.</returns>
        protected virtual IList<IViewEngineProvider> GetViewEngineProviders(IServiceLocator locator) {
            try {
                return locator.ResolveServices<IViewEngineProvider>();
            } catch {
                return null;
            }
        }

        /// <summary>
        /// Provides the auto-registration for <see cref="IViewEngine"/> types.
        /// </summary>
        /// <param name="registrationList"></param>
        public virtual void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(MvcRegistration.RegisterViewEngineProviders());
        }
    }
}