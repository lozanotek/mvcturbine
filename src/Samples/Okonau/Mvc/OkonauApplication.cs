namespace Okonau.Mvc {
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Web;
    using MvcTurbine.Ninject;

    /// <summary>
    /// Main application object for the web application to use.
    /// </summary>
    public class OkonauApplication : TurbineApplication {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static OkonauApplication() {
            // Specify the Ninject IoC to use with the application.
            ServiceLocatorManager.SetLocatorProvider(() => new NinjectServiceLocator());
        }
    }
}
