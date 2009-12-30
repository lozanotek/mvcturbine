namespace MvcTurbine.Samples.ModelBinders {
    using ComponentModel;
    using Unity;
    using Web;

    public class MvcApplication : TurbineApplication {
        static MvcApplication() {
            // Register the IoC that you want Mvc Turbine to use!
            // Everything else is wired automatically

            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator());
        }
    }
}