namespace MvcTurbine.Web.Modules {
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Web;
	using MvcTurbine.ComponentModel;

	public class HttpModuleManager : IHttpModuleManager {
		private static IList<IHttpModule> appModules;

		public HttpModuleManager(IServiceLocator locator) {
			ServiceLocator = locator;
		}

		public IServiceLocator ServiceLocator { get; private set; }

		public ReadOnlyCollection<IHttpModule> Modules {
			get { return new ReadOnlyCollection<IHttpModule>(appModules); }
		}

		public virtual void InitializeModules(HttpApplication application) {
			appModules = ServiceLocator.ResolveServices<IHttpModule>();
			if (appModules == null || appModules.Count == 0) return;

			foreach (var module in appModules) {
				module.Init(application);
			}
		}
	}
}
