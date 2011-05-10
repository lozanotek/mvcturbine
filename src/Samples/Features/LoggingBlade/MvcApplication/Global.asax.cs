namespace MvcTurbine.Samples.LoggingBlade.Web {
    using System;
    using System.Web.Routing;
    using ComponentModel;
    using MvcTurbine.Web;
    using Unity;

    public class MvcApplication : TurbineApplication {
        static MvcApplication() {
            // Register the IoC that you want Mvc Turbine to use!
            // Everything else is wired automatically

            // For now, let's use the Unity IoC
            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator());
        }
    }
}
