namespace MvcTurbine.Web.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Defines the model binder provider that provides filters that are filterable model binder.
    /// </summary>
    public class FilterableBinderProvider : IModelBinderProvider {
        private static IList<IFilterableModelBinder> registeredBinders;
        private static object _lock = new object();

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="serviceLocator"></param>
        public FilterableBinderProvider(IServiceLocator serviceLocator) {
            ServiceLocator = serviceLocator;
        }

        /// <summary>
        /// Gets the service locator associated with the provider.
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }
        
        /// <summary>
        /// Gets the binder based on the specified model.
        /// </summary>
        /// <param name="modelType">Type of model to use.</param>
        /// <returns></returns>
        public IModelBinder GetBinder(Type modelType) {
            var registeredModelBinders = GetRegisteredModelBinders();
            return registeredModelBinders == null ? null :
                registeredModelBinders.FirstOrDefault(filterableBinder => filterableBinder.SupportsModelType(modelType));
        }

        /// <summary>
        /// Gets the current registered <see cref="IModelBinder"/> instances from the container
        /// and caches them.
        /// </summary>
        /// <returns>Cached list of <see cref="IModelBinder"/>, if cache null, <see cref="IServiceLocator"/> 
        /// is queried and results are cached.</returns>
        protected virtual IList<IFilterableModelBinder> GetRegisteredModelBinders() {
            if (registeredBinders == null) {
                lock (_lock) {
                    if (registeredBinders == null) {
                        try {
                            registeredBinders = ServiceLocator.ResolveServices<IFilterableModelBinder>();
                        } catch {
                            registeredBinders = new List<IFilterableModelBinder>();
                        }
                    }
                }
            }

            return registeredBinders;
        }
    }
}
