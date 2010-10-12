namespace MvcTurbine.ComponentModel {
    using System;

    /// <summary>
    /// Provides information when an assembly can't load as a dependency.
    /// </summary>
    [Serializable]
    public class DependencyResolutionException : Exception {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="message"></param>
        public DependencyResolutionException(string assemblyName, string message) : base(message) {
            AssemblyName = assemblyName;
        }

        /// <summary>
        /// Gets or sets the assembly name that couldn't load.
        /// </summary>
        public string AssemblyName { get; set; }
    }
}