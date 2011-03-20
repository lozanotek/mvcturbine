namespace MvcTurbine.ComponentModel {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Web;

    /// <summary>
    /// Default implementation of the <see cref="IBinAssemblyLoader"/>.
    /// </summary>
    public class DefaultBinAssemblyLoader : IBinAssemblyLoader {

        /// <summary>
        /// Loads the assemblies in the bin folder that are not currently in the <see cref="AppDomain.CurrentDomain"/>.
        /// </summary>
        /// <returns>A list of assemblies that were loaded into the <see cref="AppDomain.CurrentDomain"/>.</returns>
        public virtual IList<string> LoadAssembliesFromBinFolder() {
            var currentAssemblies = GetCurrentAppDomainAssemblies();

            var assemblyFiles = GetAssembliesInBinFolder();
            var loadedAssembly = new List<string>();

            foreach (var file in assemblyFiles) {
                var assemblyName = Path.GetFileNameWithoutExtension(file);
                if (currentAssemblies.Contains(assemblyName)) continue;

                try {
                    var assembly = Assembly.LoadFrom(file);
                    loadedAssembly.Add(assembly.FullName);
                } catch (Exception exception) {
                    throw new AssemblyDependencyException(file, exception);
                }
            }

            return loadedAssembly;
        }

        /// <summary>
        /// Gets the assemblies that are currently in the <see cref="AppDomain.CurrentDomain"/>
        /// </summary>
        /// <returns>List of the simple name for the assemblies in the <see cref="AppDomain.CurrentDomain"/>.</returns>
        protected virtual IList<string> GetCurrentAppDomainAssemblies() {
            var domain = AppDomain.CurrentDomain;
            var assemblies = domain.GetAssemblies();

            var nameList = new List<string>();
            foreach (var assembly in assemblies) {
                var parts = assembly.FullName.Split(",".ToCharArray(), 
                    StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;

                var assemblyName = parts[0];
                nameList.Add(assemblyName);
            }

            return nameList;
        }

        /// <summary>
        /// Gets the assembly files (*.dll) in the <see cref="HttpRuntime.BinDirectory"/> for the application.
        /// </summary>
        /// <returns>List of paths for the assemblies in the bin folder.</returns>
        protected virtual IList<string> GetAssembliesInBinFolder() {
            var binDir = HttpRuntime.BinDirectory;
            var dllFiles = Directory.GetFiles(binDir, "*.dll");
            return new List<string>(dllFiles);
        }
    }
}
