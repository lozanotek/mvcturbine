namespace Mvc3Host {
    using MvcTurbine.Blades;
    using MvcTurbine.ComponentModel;
	using MvcTurbine.Web;
	using MvcTurbine.Web.Blades;

    public class MvcApplication : TurbineApplication {
		static MvcApplication() {
			// Now tell the engine to use the Windsor locator
			ServiceLocatorManager.SetLocatorProvider(() => new MvcTurbine.Windsor.WindsorServiceLocator());

		    CoreBlades.MetadataProvider = NullBlade.Instance;
		}
	}
}
