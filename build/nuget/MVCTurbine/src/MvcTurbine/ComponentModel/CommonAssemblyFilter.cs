namespace MvcTurbine.ComponentModel {
    using System;

    /// <summary>
    /// Defines common assemblies to filter. These assemblies are:
    ///     System, mscorlib, Microsoft, WebDev, CppCodeProvider).
    /// </summary>
    [Serializable]
    public class CommonAssemblyFilter : AssemblyFilter {
        /// <summary>
        /// Creates an instance and applies the default filters.
        /// Sets the following filters as default, (System, mscorlib, Microsoft, WebDev, CppCodeProvider).
        /// </summary>
        public CommonAssemblyFilter() {
            AddDefaults();
        }

        /// <summary>
        /// Sets the following filters as default, (System, mscorlib, Microsoft, WebDev, CppCodeProvider).
        /// </summary>
        private void AddDefaults() {
            AddFilter("System");
            AddFilter("System.Web");
            AddFilter("mscorlib");
            AddFilter("Microsoft");

            // Exclude all of the MvcTurbine core assemblies
            AddFilter("MvcTurbine,");
            AddFilter("MvcTurbine.Web,");

            // Ignore the Visual Studio extra assemblies!
            AddFilter("WebDev");
            AddFilter("CppCodeProvider");
        }
    }
}