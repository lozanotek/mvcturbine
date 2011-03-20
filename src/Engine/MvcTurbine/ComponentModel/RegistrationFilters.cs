namespace MvcTurbine.ComponentModel {
    using System;

    /// <summary>
    /// Helper methods for filtering types within the framework.
    /// </summary>
    public static class RegistrationFilters {
        private static readonly Func<Type, Type, bool> defaultFilter = BuildDefaultFilter();

        /// <summary>
        /// Gets the default filter for the system to use.
        /// </summary>
        public static Func<Type, Type, bool> DefaultFilter {
            get { return defaultFilter; }
        }

        private static Func<Type, Type, bool> BuildDefaultFilter() {
            //HACK: some types in MVC (filters, etc.) are defined as System.Attribute, so we need to ommit those.
            Type attrType = typeof (Attribute);

            return (serviceType, registrationType) =>
                   !attrType.IsAssignableFrom(serviceType) &&
                   registrationType.IsAssignableFrom(serviceType) &&
                   serviceType != registrationType &&
                   !serviceType.IsAbstract &&
                   !serviceType.IsGenericTypeDefinition &&
                   !serviceType.ContainsGenericParameters;
        }
    }
}
