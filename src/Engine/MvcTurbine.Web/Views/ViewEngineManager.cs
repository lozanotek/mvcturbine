namespace MvcTurbine.Web.Views {
    using System.Collections.Generic;
    using System.Web.Mvc;
    using MvcTurbine.ComponentModel;

    /// <summary>
    /// Default implementation of <see cref="IViewEngineManager"/>.
    /// </summary>
    public class ViewEngineManager : IViewEngineManager {
        public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="locator"></param>
        public ViewEngineManager(IServiceLocator locator) {
            ServiceLocator = locator;
        }

        /// <summary>
        /// See <see cref="IViewEngineManager.RegisterEngines"/>
        /// </summary>
        public virtual void RegisterEngines() {
            var viewEngines = GetViewEngines();

            // Add any registered ones
            if (viewEngines == null || viewEngines.Count <= 0) return;

            foreach (var viewEngine in viewEngines) {
                ViewEngines.Engines.Add(viewEngine);
            }
        }

        /// <summary>
        /// Gets the list of <see cref="IViewEngine"/> configured with the runtime.
        /// </summary>
        /// <returns></returns>
        protected virtual IList<IViewEngine> GetViewEngines() {
            try {
                return ServiceLocator.ResolveServices<IViewEngine>();
            }
            catch {
                return null;
            }
        }
    }
}