namespace MvcTurbine.Web.Filters {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Base class to provide Model (ViewModel) registration for <see cref="IModelBinder"/>.
    /// </summary>
    public abstract class GlobalFilterRegistry : IFilterRegistry {
        protected GlobalFilterRegistry() {
            FilterList = new List<FilterReg>();
        }

        /// <summary>
        /// Gets and sets the list of filter registries
        /// </summary>
        protected IList<FilterReg> FilterList { get; set; }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public virtual GlobalFilterRegistry AsGlobal<TFilter>(Action<TFilter> initializer = null)
            where TFilter : class {
            return AsGlobal(typeof(TFilter), FilterRegistryHelper.WrapInitializer(initializer));
        }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public virtual GlobalFilterRegistry AsGlobal(Type filterType, Action<object> initializer = null) {
            if (!filterType.IsMvcFilter()) {
                throw new ArgumentException("Specified argument is not an MVC filter!", "filterType");
            }

            FilterList.Add(new GlobalFilterReg { Filter = filterType, ModelInitializer = initializer });
            return this;
        }

        public IEnumerable<FilterReg> GetFilterRegistrations() {
            return FilterList;
        }
    }
}
