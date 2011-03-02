namespace MvcTurbine.Web.Filters {
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
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
            FilterList = new List<FilterReg>();
        }

        /// <summary>
        /// Gets or sets the filter list for the registry
        /// </summary>
        protected IList<FilterReg> FilterList { get; set; }

        /// <summary>
        /// Registers the <see cref="TFilter"/> with the associated action name (inferred or real) and 
        /// allows the initialization of the filter.        
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="actionName"></param>
        /// <param name="initializer"></param>
        /// <returns></returns>
        public virtual ControllerFilterRegistry<TController> ForAction<TFilter>(string actionName, Action<TFilter> initializer = null) {
            return ForAction(actionName, typeof(TFilter), FilterRegistryHelper.WrapInitializer(initializer));
        }

        /// <summary>
        /// Registers the action with the specified filter type and initializer.
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="filterType"></param>
        /// <param name="initializer"></param>
        /// <returns></returns>
        public virtual ControllerFilterRegistry<TController> ForAction(string actionName, Type filterType, Action<object> initializer = null) {
            if (!filterType.IsMvcFilter()) {
                throw new ArgumentException("Specified argument is not an MVC filter!", "filterType");
            }

            FilterList.Add(new ActionFilterReg {
                Filter = filterType,
                Controller = typeof(TController),
                Action = actionName,
                ModelInitializer = initializer
            });

            return this;
        }

        /// <summary>
        /// Registers the filter to the specified action (expression).
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual ControllerFilterRegistry<TController> ForAction<TFilter>(Expression<Action<TController>> action, Action<TFilter> initializer = null) {
            return ForAction(action, typeof (TFilter), FilterRegistryHelper.WrapInitializer(initializer));
        }

        /// <summary>
        /// Registers the filter type to the specified action (expression).
        /// </summary>
        /// <param name="action"></param>
        /// <param name="filterType"></param>
        /// <returns></returns>
        public virtual ControllerFilterRegistry<TController> ForAction(Expression<Action<TController>> action, Type filterType, Action<object> initializer) {
            var call = action.Body as MethodCallExpression;
            return ForAction(call.Method.Name, filterType, initializer);
        }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public virtual ControllerFilterRegistry<TController> With<TFilter>(Action<TFilter> initializer = null)
            where TFilter : class {
            return With(typeof(TFilter), FilterRegistryHelper.WrapInitializer(initializer));
        }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <returns></returns>
        public virtual ControllerFilterRegistry<TController> With(Type filterType, Action<object> initializer = null) {
            if (!filterType.IsMvcFilter()) {
                throw new ArgumentException("Specified argument is not an MVC filter!", "filterType");
            }

            FilterList.Add(new ControllerFilterReg {
                Filter = filterType,
                Controller = typeof(TController),
                ModelInitializer = initializer
            });

            return this;
        }

        /// <summary>
        /// Gets the list of filter registries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FilterReg> GetFilterRegistrations() {
            return FilterList;
        }
    }
}
