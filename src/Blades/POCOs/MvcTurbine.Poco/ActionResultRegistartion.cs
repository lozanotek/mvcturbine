namespace MvcTurbine.Poco {
	using System.Web.Mvc;
	using ComponentModel;

	public class ActionResultRegistartion : IServiceRegistration {
		public void Register(IServiceLocator locator) {
			locator.Register<IActionInvoker, PocoActionInvoker>();
		}
	}
}