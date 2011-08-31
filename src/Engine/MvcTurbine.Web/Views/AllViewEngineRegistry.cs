namespace MvcTurbine.Web.Views {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using MvcTurbine.ComponentModel;
    using TypeExtensions = MvcTurbine.TypeExtensions;

    /// <summary>
    /// Default implementation for <see cref="IViewEngineProvider"/> via <see cref="ViewEngineRegistry"/>.
    /// </summary>
    public class AllViewEngineRegistry : ViewEngineRegistry {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="filter"></param>
        public AllViewEngineRegistry(AssemblyFilter filter) {
            Filter = filter;
        }

        /// <summary>
        /// Gets the <see cref="AssemblyFilter"/> to use for parsing the <see cref="IViewEngine"/>.
        /// </summary>
        public AssemblyFilter Filter { get; private set; }

        /// <summary>
        /// Gets the registered <see cref="ViewEngine"/> types for the  runtime to use.
        /// </summary>
        /// <returns></returns>
        public override IList<ViewEngine> GetViewEngineRegistrations() {
            var assemblies = GetAssemblies();
            if (assemblies == null) return null;

            foreach (var assembly in assemblies) {
                var moduleQuery = assembly.GetTypes()
                    .Where(type => TypeExtensions.IsType<IViewEngine>(type))
                    .Where(type => !type.IsAbstract);

                moduleQuery.ForEach(type => Add(type));
            }

            return Engines;
        }

        /// <summary>
        /// Gets all the assemblies after the <see cref="Filter"/> property is applied.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Assembly> GetAssemblies() {
            if (Filter != null) {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var assemblyNames = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Select(asm => asm.FullName)
                    .ToList();

                var excludedNames = assemblyNames
                    .Where(assembly => Filter.Match(assembly))
                    .ToList();

                var filteredNames = assemblyNames.Except(excludedNames).ToList();

                return (from asm in assemblies
                        join asmName in filteredNames
                            on asm.FullName equals asmName
                        select asm)
                    .ToList();
            }

            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}