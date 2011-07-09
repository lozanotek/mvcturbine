namespace MvcTurbine.Web.Filters {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    ///<summary>
    /// Base registration class for filters associated with controllers. 
    ///</summary>
    ///<typeparam name="TController"></typeparam>
    public abstract class ControllerFilterRegistry<TController> : IFilterRegistry
        where TController : IController {

        /// <summary>
        /// Initializes the filter list registration.
        /// </summary>
        protected ControllerFilterRegistry() {
            FilterList = new List<Filter>();
        }

        /// <summary>
        /// Gets or sets the filter list for the registry
        /// </summary>
        protected IList<Filter> FilterList { get; set; }

        /// <summary>
        /// Applies the filter to either the controller or an action with the specified initializer.
        /// </summary>
        /// <typeparam name="TFilter">Type of the filter to register.</typeparam>
		/// <param name="initializer">Initializer code for the filter.</param>
		/// <param name="order">Order for the filter.</param>
		/// <returns></returns>
        public virtual ControllerActionExpression<TController> Apply<TFilter>(Action<TFilter> initializer = null, int order = -1) {
            return Apply(typeof(TFilter), FilterRegistryHelper.WrapInitializer(initializer), order);
        }

        /// <summary>
        /// Applies the filter to either the controller or an action with the specified initializer.
        /// </summary>
        /// <param name="filterType">Type of the filter to register.</param>
		/// <param name="initializer">Initializer code for the filter.</param>
		/// <param name="order">Order for the filter.</param>
		/// <returns></returns>
        public virtual ControllerActionExpression<TController> Apply(Type filterType, Action<object> initializer = null, int order = -1) {
            var expression = new ControllerActionExpression<TController>(FilterList, filterType, initializer, order);
            expression.Register();

            return expression;
        }

        /// <summary>
        /// Gets the list of filter registries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Filter> GetFilterRegistrations() {
            return FilterList;
        }
    }
}
