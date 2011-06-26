namespace Mvc3Host {
	using MvcTurbine.ComponentModel;
	using MvcTurbine.Web;
	using MvcTurbine.Web.Config;

	public class MvcApplication : TurbineApplication {
		static MvcApplication() {
			// Now tell the engine to use the Windsor locator
			ServiceLocatorManager.SetLocatorProvider(() => new MvcTurbine.Windsor.WindsorServiceLocator());

			Engine.Initialize.DisableHttpModuleRegistration();
		}
	}
}
