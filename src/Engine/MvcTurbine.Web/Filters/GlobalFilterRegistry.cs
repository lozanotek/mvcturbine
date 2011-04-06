namespace MvcTurbine.Web.Filters {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Base class to provide Model (ViewModel) registration for <see cref="IModelBinder"/>.
    /// </summary>
    public abstract class GlobalFilterRegistry : IFilterRegistry {
        protected GlobalFilterRegistry() {
            FilterList = new List<Filter>();
        }

        /// <summary>
        /// Gets and sets the list of filter registries
        /// </summary>
        protected IList<Filter> FilterList { get; set; }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
		/// <param name="initializer">Initializer code for the filter.</param>
		/// <param name="order">Order for the filter.</param>
        /// <returns></returns>
        public virtual GlobalFilterRegistry AsGlobal<TFilter>(Action<TFilter> initializer = null, int order = -1)
            where TFilter : class {
            return AsGlobal(typeof(TFilter), FilterRegistryHelper.WrapInitializer(initializer), order);
        }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
		/// <param name="initializer">Initializer code for the filter.</param>
		/// <param name="order">Order for the filter.</param>
		/// <returns></returns>
        public virtual GlobalFilterRegistry AsGlobal(Type filterType, Action<object> initializer = null, int order = -1) {
            if (!filterType.IsMvcFilter()) {
                throw new ArgumentException("Specified argument is not an MVC filter!", "filterType");
            }

            FilterList.Add(new GlobalFilter { FilterType = filterType, ModelInitializer = initializer, Order = order });
            return this;
        }

		/// <summary>
		/// Gets the registered filter instances.
		/// </summary>
		/// <returns></returns>
        public IEnumerable<Filter> GetFilterRegistrations() {
            return FilterList;
        }
    }
}
