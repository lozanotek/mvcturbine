namespace MvcTurbine.Web.Modules {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using ComponentModel;

    public class AllHttpModulesRegistry : HttpModuleRegistry {
        public AllHttpModulesRegistry(AssemblyFilter filter) {
            Filter = filter;
        }

        public AssemblyFilter Filter { get; private set; }

        public override IEnumerable<HttpModule> GetModuleRegistrations() {
            var assemblies = GetAssemblies();
            if (assemblies == null) return null;

            foreach (var assembly in assemblies) {
                var moduleQuery = assembly.GetTypes()
                    .Where(type => type.IsType<IHttpModule>())
                    .Where(type => !type.IsAbstract);

                var typeList = moduleQuery.Select(type => new HttpModule {Type = type}).ToList();
                typeList.ForEach(Modules.Add);
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
