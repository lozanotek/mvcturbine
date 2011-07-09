namespace MvcTurbine.Web.Modules {
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Web;
	using ComponentModel;

    /// <summary>
    /// Default implementation of <see cref="IHttpModuleManager"/>.
    /// </summary>
	public class HttpModuleManager : IHttpModuleManager {
		private static IList<IHttpModule> appModules;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="locator"></param>
		public HttpModuleManager(IServiceLocator locator) {
			ServiceLocator = locator;
		}

        /// <summary>
        /// Gets the <see cref="IServiceLocator"/> associated with the class.
        /// </summary>
		public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Gets a list of all the <see cref="IHttpModule"/> objects used by the runtime.
        /// </summary>
		public ReadOnlyCollection<IHttpModule> Modules {
			get { return new ReadOnlyCollection<IHttpModule>(appModules); }
		}

        /// <summary>
        /// Initializes all the <see cref="Modules"/> with the application.
        /// </summary>
        /// <param name="application"></param>
		public virtual void InitializeModules(HttpApplication application) {
			appModules = ServiceLocator.ResolveServices<IHttpModule>();
			if (appModules == null || appModules.Count == 0) return;

			foreach (var module in appModules) {
				module.Init(application);
			}
		}

        /// <summary>
        /// Disposes the <see cref="Modules"/> with the application.
        /// </summary>
        /// <param name="application"></param>
		public void DisposeModules(HttpApplication application) {
            if (appModules == null || appModules.Count == 0) return;

            foreach (var httpModule in appModules) {
                if (httpModule == null) continue;
				httpModule.Dispose();
			}
		}
	}
}
