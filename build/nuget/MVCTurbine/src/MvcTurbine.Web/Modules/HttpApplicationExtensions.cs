namespace MvcTurbine.Web.Modules {
	using System.Collections.ObjectModel;
	using System.Web;
	using ComponentModel;

	/// <summary>
	/// Extension methods for the <see cref="HttpApplication"/>.
	/// </summary>
	public static class HttpApplicationExtensions {
		/// <summary>
		/// Gets the <see cref="IServiceLocator"/> that's associated with the application.
		/// </summary>
		/// <param name="application"></param>
		/// <returns></returns>
		public static IServiceLocator ServiceLocator(this HttpApplication application) {
			var turbineApplication = application as ITurbineApplication;
			return turbineApplication == null ? null : turbineApplication.ServiceLocator;
		}

		/// <summary>
		/// Gets the <see cref="IHttpModule"/> objects that were loaded via <see cref="IHttpModuleRegistry"/> types.
		/// </summary>
		/// <param name="application"></param>
		/// <returns></returns>
		public static ReadOnlyCollection<IHttpModule> DynamicModules(this HttpApplication application) {
			var locator = ServiceLocator(application);
			if (locator == null) return null;

			var moduleManager = locator.Resolve<IHttpModuleManager>();
			return moduleManager == null ? null : moduleManager.Modules;
		}
	}
}
