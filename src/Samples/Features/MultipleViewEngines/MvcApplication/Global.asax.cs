namespace MvcTurbine.Samples.MultipleViewEngines {
    using ComponentModel;
    using Web;
    using Windsor;

    public class MvcApplication : TurbineApplication {
        static MvcApplication() {
            // Register the IoC that you want Mvc Turbine to use!
            // Everything else is wired automatically

            // For now, let's use the Castle Windsor IoC since it will resolve the view engine's
            // default constructor
            ServiceLocatorManager.SetLocatorProvider(() => new WindsorServiceLocator());
        }
    }
}