namespace MvcTurbine.ComponentModel {

    ///<summary>
    /// Defines the process of doing auto registration of a specified service type.
    ///</summary>
    public interface IAutoRegistrator {
        /// <summary>
        /// Gets or sets the <seealso cref="AssemblyFilter"/> to use.
        /// </summary>
        AssemblyFilter Filter { get; set; }

        /// <summary>
        /// Process the specified <seealso cref="ServiceRegistration"/> for the types in all assemblies.
        /// </summary>
        /// <param name="serviceRegistration">Instance of <see cref="ServiceRegistration"/> to use.</param>
        void AutoRegister(ServiceRegistration serviceRegistration);
    }
}