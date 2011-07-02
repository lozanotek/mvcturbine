namespace MvcTurbine.Web.Modules {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using ComponentModel;

    /// <summary>
    /// Defines the way to register all <see cref="IHttpModule"/> for the runtime.
    /// </summary>
    public class AllHttpModulesRegistry : HttpModuleRegistry {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filter"></param>
        public AllHttpModulesRegistry(AssemblyFilter filter) {
            Filter = filter;
        }

        /// <summary>
        /// Gets the <see cref="AssemblyFilter"/> to use for parsing the <see cref="IHttpModule"/>.
        /// </summary>
        public AssemblyFilter Filter { get; private set; }

        /// <summary>
        /// Gets the registered <see cref="HttpModule"/> types for the  runtime to use.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<HttpModule> GetModuleRegistrations() {
            var assemblies = GetAssemblies();
            if (assemblies == null) return null;

            foreach (var assembly in assemblies) {
                var moduleQuery = assembly.GetTypes()
                    .Where(type => type.IsType<IHttpModule>())
                    .Where(type => !type.IsAbstract);

                moduleQuery.ForEach(type => Add(type));
            }

            return Modules;
        }

        /// <summary>
        /// Gets all the assemblies after the <see cref="Filter"/> property is applied.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Assembly> GetAssemblies() {
            if (Filter != null) {
                return AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(assembly => !Filter.Match(assembly.FullName));
            }

            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}
