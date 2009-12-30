namespace NerdDinner {
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Unity;
    using MvcTurbine.Web;

    public class MvcApplication : TurbineApplication {
        static MvcApplication() {
            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator());
        }
    }
}