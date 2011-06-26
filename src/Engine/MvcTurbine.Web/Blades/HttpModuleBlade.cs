namespace MvcTurbine.Web.Blades {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Modules;
	using ComponentModel;
	using System.Web;

	/// <summary>
	/// Blade for all http modules with auto-registration
	/// </summary>
	public class HttpModuleBlade : CoreBlade, ISupportAutoRegistration {
		public void AddRegistrations(AutoRegistrationList registrationList) {
			registrationList.Add(Registration.Simple<IHttpModuleRegistry>());
		}

		public override void Spin(IRotorContext context) {
			var locator = GetServiceLocatorFromContext(context);
			var moduleRegistries = GetModuleRegistries(locator) ?? new List<IHttpModuleRegistry>();
			var filteredList = GetFilteredList(moduleRegistries);

		    using (locator.Batch()) {
				foreach (var type in filteredList) {
					locator.Register(type, type);
				}
			}
		}

	    protected virtual IList<Type> GetFilteredList(IList<IHttpModuleRegistry> moduleRegistries) {
	        var allInclude = new List<Type>();
	        var allExclude = new List<Type>();

	        foreach (var registry in moduleRegistries) {
	            var moduleRegistration = registry.GetModuleRegistrations();

	            var includeList = moduleRegistration
	                .Where(reg => !reg.IsRemoved)
	                .Select(reg => reg.Type)
	                .ToList();

	            var excludeList = moduleRegistration
	                .Where(reg => reg.IsRemoved)
	                .Select(reg => reg.Type)
	                .ToList();

	            allInclude.AddRange(includeList);
	            allExclude.AddRange(excludeList);
	        }

	        return allInclude.Except(allExclude).ToList();
	    }

	    protected IList<IHttpModuleRegistry> GetModuleRegistries(IServiceLocator locator) {
			try {
				return locator.ResolveServices<IHttpModuleRegistry>();
			}
			catch {
				return null;
			}
		}
	}
}
