namespace MvcTurbine.Web.Blades {
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using Modules;
	using ComponentModel;

	/// <summary>
	/// Blade for all http modules with auto-registration
	/// </summary>
	public class HttpModuleBlade : CoreBlade, ISupportAutoRegistration {

        /// <summary>
        /// Defines auto-registration for <see cref="IHttpModuleRegistry"/> types.
        /// </summary>
        /// <param name="registrationList"></param>
		public void AddRegistrations(AutoRegistrationList registrationList) {
			registrationList.Add(Registration.Simple<IHttpModuleRegistry>());
		}

        /// <summary>
        /// Registers all <see cref="IHttpModule"/> defined through <see cref="IHttpModuleRegistry"/> with the runtime.
        /// </summary>
        /// <param name="context"></param>
		public override void Spin(IRotorContext context) {
			var locator = GetServiceLocatorFromContext(context);
			var moduleRegistries = GetModuleRegistries(locator);

            if (moduleRegistries == null) return;
			var filteredList = GetFilteredList(moduleRegistries);

		    using (locator.Batch()) {
				foreach (var module in filteredList) {
				    locator.Register(typeof (IHttpModule), module.Type, module.Name);
				}
			}
		}

        /// <summary>
        /// Filters the list of <see cref="HttpModule"/> from those that have been added/removed from the system 
        /// across all <see cref="IHttpModuleRegistry"/> with the system.
        /// </summary>
        /// <param name="moduleRegistries"></param>
        /// <returns></returns>
	    protected virtual IList<HttpModule> GetFilteredList(IList<IHttpModuleRegistry> moduleRegistries) {
            var allInclude = new List<HttpModule>();
            var allExclude = new List<HttpModule>();

	        foreach (var registry in moduleRegistries) {
	            var moduleRegistration = registry.GetModuleRegistrations();

                if (moduleRegistration == null) continue;

	            var includeList = moduleRegistration
	                .Where(reg => !reg.IsRemoved)
	                .ToList();

	            var excludeList = moduleRegistration
	                .Where(reg => reg.IsRemoved)
	                .ToList();

	            allInclude.AddRange(includeList);
	            allExclude.AddRange(excludeList);
	        }

	        return allInclude.Except(allExclude).ToList();
	    }

        /// <summary>
        /// Gets all <see cref="IHttpModuleRegistry"/> types registered with the system.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
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
