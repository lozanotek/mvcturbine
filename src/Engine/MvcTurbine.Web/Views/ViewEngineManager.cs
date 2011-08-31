namespace MvcTurbine.Web.Views {
    using System.Collections.Generic;
    using System.Web.Mvc;
    using MvcTurbine.ComponentModel;

    public class ViewEngineManager : IViewEngineManager {
        public IServiceLocator ServiceLocator { get; private set; }

        public ViewEngineManager(IServiceLocator locator) {
            ServiceLocator = locator;
        }

        public virtual void RegisterEngines() {
            var viewEngines = GetViewEngines();

            // Add any registered ones
            if (viewEngines == null || viewEngines.Count <= 0) return;

            foreach (var viewEngine in viewEngines) {
                ViewEngines.Engines.Add(viewEngine);
            }
        }

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