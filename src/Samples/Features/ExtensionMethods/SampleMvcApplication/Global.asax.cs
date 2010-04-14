namespace MvcTurbine.Samples.ExtensionMethods {
    using ComponentModel;
    using Ninject;
    using Web;

    public class MvcApplication : TurbineApplication {
        static MvcApplication() {
            // Register the IoC that you want Mvc Turbine to use!
            // Everything else is wired automatically

            // For now, let's use the Unity IoC
            ServiceLocatorManager.SetLocatorProvider(() => new NinjectServiceLocator());
        }
    }
}