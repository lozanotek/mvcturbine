namespace MvcTurbine.Ninject {
    using System.Web;
    using global::Ninject;
    using MvcTurbine.Web.Modules;

    /// <summary>
    /// Defines the <see cref="IHttpModule"/> types to exclude during runtime.
    /// </summary>
    public class NinjectModuleRegistry : HttpModuleRegistry {
        /// <summary>
        /// Default constructor
        /// </summary>
        public NinjectModuleRegistry() {
            //Remove the HttpModule that enables one instance per request.
            Remove<OnePerRequestModule>();
        }
    }
}