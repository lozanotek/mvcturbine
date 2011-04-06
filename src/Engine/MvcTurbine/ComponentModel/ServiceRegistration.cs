namespace MvcTurbine.ComponentModel {
    using System;

    ///<summary>
    /// Defines a registration for a service within application.
    ///</summary>
    [Serializable]
    public class ServiceRegistration {
        /// <summary>
        /// Gets or sets the type of the service to use.
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the actual registration handler for the service
        /// </summary>
        public Action<IServiceLocator, Type> RegistrationHandler { get; set; }

        /// <summary>
        /// Gets or sets the filter, if any, to use for the types.
        /// </summary>
        public Func<Type, Type, bool> TypeFilter { get; set; }

        /// <summary>
        /// Checks wether the instance is valid for processing
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            return (ServiceType != null && RegistrationHandler != null) && TypeFilter != null;
        }
    }
}