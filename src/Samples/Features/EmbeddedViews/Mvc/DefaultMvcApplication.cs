namespace Mvc {
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Web;
    using MvcTurbine.Windsor;

    public class DefaultMvcApplication : TurbineApplication {
        static DefaultMvcApplication() {
            ServiceLocatorManager.SetLocatorProvider(() => new WindsorServiceLocator());
        }
    }
}