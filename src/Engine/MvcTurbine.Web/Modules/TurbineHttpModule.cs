namespace MvcTurbine.Web.Modules {
	using System;
	using System.Web;
	using MvcTurbine.ComponentModel;

	/// <summary>
	/// Module used to initialize all modules auto-registered with the engine.
	/// </summary>
	public class TurbineHttpModule : IHttpModule {
		private static readonly object _lock = new object();
		private static IHttpModuleManager moduleManager;

		/// <summary>
		/// Initializes all the registered <see cref="IHttpModule"/> instances.
		/// </summary>
		/// <remarks>
		/// This code has to live here in order for the pieces to work correctly with
		/// the ASP.NET runtime on IIS6/7.
		/// </remarks>
		public void Init(HttpApplication context) {
			if (context == null) return;

			var locator = context.ServiceLocator();
			var manager = GetModuleManager(locator);

			if (manager == null) return;
			manager.InitializeModules(context);
		}

		/// <summary>
		/// Disposes this and other auto-registered modules.
		/// </summary>
		public void Dispose() {
			// We should already have it
			if (moduleManager == null || moduleManager.Modules == null) return;

			foreach (var httpModule in moduleManager.Modules) {
				var disposableModule = httpModule as IDisposable;

				if (disposableModule == null) continue;
				disposableModule.Dispose();
			}

			moduleManager = null;
		}

		/// <summary>
		/// Gets the instance of <see cref="IHttpModuleManager"/> that is registered with the <see cref="IServiceLocator"/>.
		/// </summary>
		/// <returns>The registered <see cref="IHttpModuleManager"/>, otherwise a default <see cref="IHttpModuleManager"/> is used.</returns>
		protected virtual IHttpModuleManager GetModuleManager(IServiceLocator locator){
			if (moduleManager == null) {
				lock (_lock) {
					if (moduleManager == null) {
						try {
							moduleManager = locator.Resolve<IHttpModuleManager>();
						}
						catch {
							moduleManager = new HttpModuleManager(locator);
						}
					}
				}
			}

			return moduleManager;
		}
	}
}
