namespace MvcTurbine.Web.Blades {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ComponentModel;
    using Filters;
    using MvcTurbine.Blades;
    using Filter = MvcTurbine.Web.Filters.Filter;

    /// <summary>
    /// Default <see cref="IBlade"/> that supports all ASP.NET MVC components.
    /// </summary>
	public class FilterBlade : CoreBlade, ISupportAutoRegistration {
        /// <summary>
        /// Provides the auto-registration for <see cref="IFilterProvider"/> and <see cref="IFilterRegistry"/> types.
        /// </summary>
        /// <param name="registrationList"></param>
        public virtual void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList
                .Add(Registration.Simple<IFilterProvider>())
                .Add(Registration.Simple<IFilterRegistry>());
        }

        ///<summary>
        /// Sets up the <see cref="IFilterProvider"/>s that have been registered with the system. Also, injects the one from
        /// MVC Turbine.
        ///</summary>
        ///<param name="context">Current <see cref="IRotorContext"/> performing the execution.</param>
        public override void Spin(IRotorContext context) {
            // Get the current IServiceLocator
            var serviceLocator = GetServiceLocatorFromContext(context);

            SetupFilterProviders(serviceLocator);
            SetupFilterRegistries(serviceLocator);
        }

        /// <summary>
        /// Queries the <see cref="IServiceLocator"/> instance for any instances of <see cref="IFilterRegistry"/> to process.
        /// </summary>
        /// <param name="serviceLocator">Current <see cref="IServiceLocator"/> instance for the application.</param>
        public virtual void SetupFilterRegistries(IServiceLocator serviceLocator) {
            var filterRegistries = GetFilterRegistries(serviceLocator);
            if (filterRegistries == null) return;

            var filterList = new List<Filter>();
            var typeList = new List<Type>();

            foreach (var filterRegistry in filterRegistries) {
                var registrations = filterRegistry.GetFilterRegistrations();

                using (serviceLocator.Batch()) {
                    foreach (var registration in registrations) {
                        var filterType = registration.FilterType;

                        // Prevent double registration of the same filter
                        if (typeList.Contains(filterType)) continue;

                        serviceLocator.Register(filterType, filterType);
                        typeList.Add(filterType);
                    }
                }

                filterList.AddRange(registrations);
            }

            typeList.Clear();
            FilterProviders.Providers.Add(new FilterRegistryProvider(serviceLocator, filterList));
        }

        ///<summary>
        /// Links the Turbine specific service providers, filter providers that been registered with the container
        /// and the default ones from the MVC runtime.
        ///</summary>
        /// <param name="serviceLocator">Current <see cref="IServiceLocator"/> instance for the application.</param>
        public virtual void SetupFilterProviders(IServiceLocator serviceLocator) {
            // Clear out what's there by default
            FilterProviders.Providers.Clear();

            // Wire the pieces up just like it comes out of the box
            FilterProviders.Providers.Add(GlobalFilters.Filters);
            FilterProviders.Providers.Add(new ControllerInstanceFilterProvider());

            if (serviceLocator is IServiceInjector) {
                FilterProviders.Providers.Add(new InjectableAttributeFilterProvider(serviceLocator as IServiceInjector));
            }

            // Get the providers that were registered with the service locator
            var filterProviders = GetFilterProviders(serviceLocator);
            if (filterProviders == null || filterProviders.Count == 0) return;

            foreach (var filterProvider in filterProviders) {
                FilterProviders.Providers.Add(filterProvider);
            }
        }

        /// <summary>
        /// Gets all registered <see cref="IFilterRegistry"/> from the container.
        /// </summary>
        /// <param name="locator">Current <see cref="IServiceLocator"/> instance for the application.</param>
        /// <returns>A list of <see cref="IFilterRegistry"/>, null if instances could not be resolved.</returns>
        protected virtual IList<IFilterRegistry> GetFilterRegistries(IServiceLocator locator) {
            try {
                return locator.ResolveServices<IFilterRegistry>();
            }
            catch {
                return null;
            }
        }

        /// <summary>
        /// Gets all registered <see cref="IFilterProvider"/> from the container.
        /// </summary>
        /// <param name="locator">Current <see cref="IServiceLocator"/> instance for the application.</param>
        /// <returns>A list of <see cref="IFilterProvider"/>, null if instances could not be resolved.</returns>
        protected virtual IList<IFilterProvider> GetFilterProviders(IServiceLocator locator) {
            try {
                return locator.ResolveServices<IFilterProvider>();
            }
            catch {
                return null;
            }
        }
    }
}
