namespace Mvc3Host {
	using MvcTurbine.ComponentModel;
	using MvcTurbine.Web;

    public class MvcApplication : TurbineApplication {
		static MvcApplication() {
			// Now tell the engine to use the Windsor locator
			ServiceLocatorManager.SetLocatorProvider(() => new MvcTurbine.StructureMap.StructureMapServiceLocator());
		}
	}
}
