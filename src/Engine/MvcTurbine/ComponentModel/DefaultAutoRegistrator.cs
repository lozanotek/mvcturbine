namespace MvcTurbine.ComponentModel {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Default implementation of <seealso cref="IAutoRegistrator"/>.
    /// </summary>
    public class DefaultAutoRegistrator : IAutoRegistrator {
        private static readonly object _lock = new object();

        /// <summary>
        /// Creates an instance with the specified <seealso cref="IServiceLocator"/> implementation.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="filter"></param>
        public DefaultAutoRegistrator(IServiceLocator locator, AssemblyFilter filter) {
            ServiceLocator = locator;
            Filter = filter;
        }

        /// <summary>
        /// Gets the <seealso cref="IServiceLocator"/> associated with this instance.
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Gets or sets the <seealso cref="AssemblyFilter"/> to use.
        /// </summary>
        public AssemblyFilter Filter { get; set; }

        /// <summary>
        /// Process the specified <seealso cref="ServiceRegistration"/> for the types in all assemblies.
        /// </summary>
        /// <param name="serviceRegistration">Instance of <see cref="ServiceRegistration"/> to use.</param>
        public void AutoRegister(ServiceRegistration serviceRegistration) {
            if (serviceRegistration == null ||
                !serviceRegistration.IsValid()) return;

            lock (_lock) {
                var assemblies = GetAssemblies();
                var registration = serviceRegistration.RegistrationHandler;
                var serviceType = serviceRegistration.ServiceType;
                var typeFilter = serviceRegistration.TypeFilter;

                foreach (Assembly assembly in assemblies) {
                    try {
                        var registrationTypes = assembly.GetTypes()
                            .Where(matchedType => typeFilter(matchedType, serviceType));
                        
                        foreach (Type type in registrationTypes) {
                            registration(ServiceLocator, type);
                        }
                    } catch (ReflectionTypeLoadException loadException) {
                        string assemblyName = assembly.FullName;
                        string detailedMessage = loadException.GetDetailedMessage(assemblyName);

                        throw new DependencyResolutionException(assemblyName, detailedMessage);
                    }
                }
            }
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