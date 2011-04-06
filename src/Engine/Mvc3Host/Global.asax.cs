namespace Mvc3Host {
	using Castle.Windsor;
	using Castle.MicroKernel.Registration;

	using MvcTurbine.ComponentModel;
	using MvcTurbine.Web;
	using MvcTurbine.Web.Config;

	public class MvcApplication : TurbineApplication {
		static MvcApplication() {
			// tell the engine to use this RotorContext instance
			Engine.Initialize
				.RotorContext<CustomRotorContext>();

			// Now tell the engine to use the Windsor locator
			ServiceLocatorManager.SetLocatorProvider(() => new MvcTurbine.Ninject.NinjectServiceLocator());
		}
	}

	public class CustomRotorContext : RotorContext {
		public CustomRotorContext(IServiceLocator locator) : base(locator) {
			// very important logic goes here
		}
	}
}
