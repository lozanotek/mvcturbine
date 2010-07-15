namespace Mvc {
    using MvcTurbine.ComponentModel;
    using MvcTurbine.StructureMap;
    using MvcTurbine.Web;
    using StructureMap;

    /// <summary>
    /// Common class that can be used across all sites
    /// </summary>
    public class StructureMapTurbineApplication : TurbineApplication {
        public virtual IContainer CreateContainer() {
            // Register the types here
            ObjectFactory.Initialize(DefaultInitialization);

            return ObjectFactory.Container;
        }

        public virtual void DefaultInitialization(IInitializationExpression expression) {
        }

        protected void Application_EndRequest() {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }

        public override void Startup() {
            var container = CreateContainer();
            ServiceLocatorManager.SetLocatorProvider(() => new StructureMapServiceLocator(container));
        }
    }
}