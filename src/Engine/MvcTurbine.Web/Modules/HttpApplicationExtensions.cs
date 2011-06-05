namespace MvcTurbine.Web.Modules {
	using System.Web;
	using MvcTurbine.ComponentModel;

	public static class HttpApplicationExtensions {
		public static IServiceLocator ServiceLocator(this HttpApplication application) {
			var turbineApplication = application as ITurbineApplication;
			if (turbineApplication == null) return null;

			return turbineApplication.ServiceLocator;
		}
	}
}