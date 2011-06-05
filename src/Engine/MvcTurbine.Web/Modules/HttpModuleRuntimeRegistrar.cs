using System.Web;
using MvcTurbine.Web.Modules;

[assembly: PreApplicationStartMethod(typeof(HttpModuleRuntimeRegistrar), "RuntimeRegister")]

namespace MvcTurbine.Web.Modules {
	using Microsoft.Web.Infrastructure.DynamicModuleHelper;

	public class HttpModuleRuntimeRegistrar {
		public static void RuntimeRegister() {
			DynamicModuleUtility.RegisterModule(typeof(TurbineHttpModule));	
		}
	}
}