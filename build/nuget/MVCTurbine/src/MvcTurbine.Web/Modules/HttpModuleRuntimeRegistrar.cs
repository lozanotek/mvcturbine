using System.Web;
using MvcTurbine.Web.Modules;

[assembly: PreApplicationStartMethod(typeof(HttpModuleRuntimeRegistrar), "RuntimeRegister")]

namespace MvcTurbine.Web.Modules {
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;

	/// <summary>
	/// Registers a startup method with the ASP.NET Runtime.
	/// </summary>
	public class HttpModuleRuntimeRegistrar {
		/// <summary>
		/// Register the <see cref="TurbineHttpModule"/> dynamically with the runtime.
		/// </summary>
		public static void RuntimeRegister() {
			DynamicModuleUtility.RegisterModule(typeof(TurbineHttpModule));	
		}
	}
}