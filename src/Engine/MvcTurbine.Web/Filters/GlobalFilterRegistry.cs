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

        protected IList<FilterReg> FilterList { get; set; }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public virtual GlobalFilterRegistry AsGlobal<TFilter>()
            where TFilter : class {
                return AsGlobal(typeof(TFilter));
        }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public virtual GlobalFilterRegistry AsGlobal(Type filterType) {
            if (!filterType.IsMvcFilter()) {
                throw new ArgumentException("Specified argument is not an MVC filter!", "filterType");
            }

            FilterList.Add(new GlobalFilterReg { Filter = filterType });
            return this;
        }

        public IEnumerable<FilterReg> GetFilterRegistrations() {
            return FilterList;
        }
    }
}