namespace Mvc {
    using MvcTurbine.ComponentModel;
    using MvcTurbine.StructureMap;
    using MvcTurbine.Web;
    using StructureMap;

    public class DefaultMvcApplication : TurbineApplication {
        static DefaultMvcApplication() {
            ServiceLocatorManager.SetLocatorProvider(() => new StructureMapServiceLocator());
        }

        protected void Application_EndRequest() {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }
}
