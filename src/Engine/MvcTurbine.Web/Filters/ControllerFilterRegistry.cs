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
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="initializer"></param>
        /// <returns></returns>
        public virtual ControllerActionExpression<TController> Apply<TFilter>(Action<TFilter> initializer = null) {
            return Apply(typeof(TFilter), FilterRegistryHelper.WrapInitializer(initializer));
        }

        /// <summary>
        /// Applies the filter to either the controller or an action with the specified initializer.
        /// </summary>
        /// <param name="filterType"></param>
        /// <param name="initializer"></param>
        /// <returns></returns>
        public virtual ControllerActionExpression<TController> Apply(Type filterType, Action<object> initializer = null) {
            var expression = new ControllerActionExpression<TController>(FilterList, filterType, initializer);
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
