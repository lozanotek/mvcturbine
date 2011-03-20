namespace MvcTurbine.ComponentModel {
    using System;

    /// <summary>
    /// Helper methods for registration of services with the runtime.
    /// </summary>
    public static class Registration {
        /// <summary>
        /// Creates a registration for <see cref="TService"/> with <see cref="RegistrationFilters.DefaultFilter"/>.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static ServiceRegistration Simple<TService>()
            where TService : class {
            return Simple<TService>(RegistrationFilters.DefaultFilter);
        }

        /// <summary>
        /// Creates a registration for <see cref="TService"/> with the specified filter. 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static ServiceRegistration Simple<TService>(Func<Type, Type, bool> filter)
            where TService : class {
            return Custom<TService>(filter,
                                    (locator, type) => locator.Register<TService>(type));
        }

        /// <summary>
        /// Creates a keyed registration for <see cref="TService"/> with <see cref="RegistrationFilters.DefaultFilter"/>.
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static ServiceRegistration Keyed<TService>()
            where TService : class {
            return Keyed<TService>(RegistrationFilters.DefaultFilter);
        }

        /// <summary>
        /// Creates a keyed registration for <see cref="TService"/> with the specified filter.        
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static ServiceRegistration Keyed<TService>(Func<Type, Type, bool> filter)
            where TService : class {
            return Custom<TService>(RegistrationFilters.DefaultFilter,
                                    (locator, type) => locator.Register(type.FullName.ToLower(), type));
        }

        /// <summary>
        /// Creates a custom registration for <see cref="TService"/> with the specified filter. 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="filter"></param>
        /// <param name="regAction"></param>
        /// <returns></returns>
        public static ServiceRegistration Custom<TService>(Func<Type, Type, bool> filter, Action<IServiceLocator, Type> regAction)
            where TService : class {

            return new ServiceRegistration {
                TypeFilter = filter,
                RegistrationHandler = regAction,
                ServiceType = typeof(TService)
            };
        }
    }
}