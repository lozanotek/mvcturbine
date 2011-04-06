namespace MvcTurbine.ComponentModel {
    using System;
    using System.Reflection;

    ///<summary>
    /// Defines an error when an <see cref="Assembly"/> cannot be loaded.
    ///</summary>
    [Serializable]
    public class AssemblyDependencyException : Exception {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="innerException"></param>
        public AssemblyDependencyException(string assemblyFile, Exception innerException)
            : base(string.Format("Could not load assembly file '{0}'", assemblyFile), innerException) 
        {
            AssemblyFile = assemblyFile;
        }

        /// <summary>
        /// Gets or sets the assembly name that couldn't load.
        /// </summary>
        public string AssemblyFile { get; set; }
    }
}