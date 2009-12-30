namespace MvcTurbine.Samples.LoggingBlade.Web {
    using ComponentModel;
    using MvcTurbine.Web;
    using StructureMap;

    //using Unity;

    public class MvcApplication : TurbineApplication {
        static MvcApplication() {
            // Register the IoC that you want Mvc Turbine to use!
            // Everything else is wired automatically

            // For now, let's use the Unity IoC
            ServiceLocatorManager.SetLocatorProvider(() => new StructureMapServiceLocator());
        }
    }
}