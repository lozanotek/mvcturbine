using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using MvcTurbine.ComponentModel;
using MvcTurbine.Web;
using MvcTurbine.Web.Config;

namespace MvcHost {
	public class MvcApplication : TurbineApplication {
		static MvcApplication() {
			// Now tell the engine to use the Windsor locator
			//ServiceLocatorManager.SetLocatorProvider(() => new MvcTurbine.Windsor.WindsorServiceLocator());
			ServiceLocatorManager.SetLocatorProvider(() => new MvcTurbine.Ninject.NinjectServiceLocator());
			//ServiceLocatorManager.SetLocatorProvider(() => new MvcTurbine.StructureMap.StructureMapServiceLocator());
			//ServiceLocatorManager.SetLocatorProvider(() => new MvcTurbine.Unity.UnityServiceLocator());

			Engine.Initialize
				.OnStartUp(AppStartup)
				.DisableViewEngineRegistration();
		}

		public static void AppStartup() {
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			// This is not needed since the RoutingBlade will handle all the pieces.
			//RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
		}
	}
}
