namespace MvcTurbine.ComponentModel {
    using System;

    /// <summary>
    /// Defines the missing resolution of services within the <see cref="IServiceLocator"/>.
    /// </summary>
    [Serializable]
    public class ServiceResolutionException : Exception {
        /// <summary>
        ///  Creates an exception with the specified type.
        /// </summary>
        /// <param name="service"></param>
        public ServiceResolutionException(Type service) :
            base(string.Format("Could not resolve serviceType '{0}'", service)) {
            ServiceType = service;
        }

        /// <summary>
        /// Creates an exception with the specified type and inner exception. 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="innerException"></param>
        public ServiceResolutionException(Type service, Exception innerException)
            : base(string.Format("Could not resolve serviceType '{0}'", service), innerException) {
            ServiceType = service;
        }

        /// <summary>
        /// Gets or sets the type of the service to use.
        /// </summary>
        public Type ServiceType { get; set; }
    }
}
