namespace MvcTurbine.Web.Modules {
	using System.Collections.ObjectModel;
	using System.Web;

    /// <summary>
    /// Defines the intialization and disposing of <see cref="IHttpModule"/> in the runtime.
    /// </summary>
	public interface IHttpModuleManager {
        /// <summary>
        /// Gets the <see cref="IHttpModule"/> types dynamically registered with the system.
        /// </summary>
		ReadOnlyCollection<IHttpModule> Modules { get; }

        /// <summary>
        /// Initializes all the <see cref="IHttpModule"/> with the system.
        /// </summary>
        /// <param name="application"></param>
		void InitializeModules(HttpApplication application);

        /// <summary>
        /// Disposes all the <see cref="IHttpModule"/> with the system.
        /// </summary>
        /// <param name="application"></param>
	    void DisposeModules(HttpApplication application);
	}
}
