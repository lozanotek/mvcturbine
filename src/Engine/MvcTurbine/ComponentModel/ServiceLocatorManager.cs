namespace MvcTurbine.ComponentModel {
    using System;

    /// <summary>
    /// Defines the resolutioon delegate to obtain an instance of <see cref="IServiceLocator"/>.
    /// </summary>
    /// <remarks>
    /// This class is modelled after the ServiceLocator class of the Common ServiceLocator hosted
    /// on CodePlex at http://commonservicelocator.codeplex.com
    /// </remarks>
    /// <returns></returns>
    public delegate IServiceLocator ServiceLocatorProvider();

    /// <summary>
    /// Utility class for accessing the default registered instance of <see cref="IServiceLocator"/>.
    /// </summary>
    public static class ServiceLocatorManager {
        private static ServiceLocatorProvider currentProvider;
        private static IServiceLocator serviceLocator;
        private static readonly object _lock = new object();

        /// <summary>
        /// Sets the current instance of <see cref="IServiceLocator"/> by using the specified
        /// resolution delegate.
        /// </summary>
        /// <param name="newProvider">Resolution delegate that will obtain the instance of 
        /// <see cref="IServiceLocator"/>.</param>
        public static void SetLocatorProvider(ServiceLocatorProvider newProvider) {
            currentProvider = newProvider;
        }

        /// <summary>
        /// Gets the current registered instance of <see cref="IServiceLocator"/>.
        /// </summary>
        /// <remarks>To register an instance use the <see cref="SetLocatorProvider"/> method.</remarks>
        public static IServiceLocator Current {
            get {
                if (currentProvider == null) {
                    throw new InvalidOperationException(Properties.Resources.ServiceLocatorProviderExceptionMessage);
                }

                if (serviceLocator == null) {
                    lock (_lock) {
                        if (serviceLocator == null) {
                            lock (_lock) {
                                serviceLocator = currentProvider();
                            }
                        }
                    }
                }

                return serviceLocator;
            }
        }
    }
}
