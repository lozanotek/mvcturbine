namespace WebTest {
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Web;
    using MvcTurbine.Windsor;

    public class MvcApplication : TurbineApplication {
        static MvcApplication() {
            ServiceLocatorManager.SetLocatorProvider(() => new WindsorServiceLocator());
        }
    }
}