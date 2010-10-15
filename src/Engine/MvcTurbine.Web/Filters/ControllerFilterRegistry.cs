namespace MvcTurbine.Web.Filters {
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    ///<summary>
    ///</summary>
    ///<typeparam name="TController"></typeparam>
    public abstract class ControllerFilterRegistry<TController> : IFilterRegistry
        where TController : IController {

        protected ControllerFilterRegistry() {
            FilterList = new List<FilterReg>();
        }

        protected IList<FilterReg> FilterList { get; set; }

        public virtual ControllerFilterRegistry<TController> ForAction<TFilter>(Expression<Action<TController>> action) {
            return ForAction(action, typeof(TFilter));
        }

        public virtual ControllerFilterRegistry<TController> ForActions<TFilter>(params Expression<Action<TController>>[] actions) {
            if (actions == null) {
                throw new ArgumentException("Please specify at least one action.");
            }

            foreach (var action in actions) {
                ForAction(action, typeof(TFilter));
            }

            return this;
        }

        public virtual ControllerFilterRegistry<TController> ForAction(Expression<Action<TController>> action, Type filterType) {
            if (!filterType.IsMvcFilter()) {
                throw new ArgumentException("Specified argument is not an MVC filter!", "filterType");
            }

            var call = action.Body as MethodCallExpression;

            FilterList.Add(new ActionFilterReg { Filter = filterType, Controller = typeof(TController), Action = call.Method.Name });
            return this;
        }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public virtual ControllerFilterRegistry<TController> With<TFilter>()
            where TFilter : class {
            return With(typeof(TFilter));
        }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <returns></returns>
        public virtual ControllerFilterRegistry<TController> With(Type filterType) {
            if (!filterType.IsMvcFilter()) {
                throw new ArgumentException("Specified argument is not an MVC filter!", "filterType");
            }

            FilterList.Add(new ControllerFilterReg { Filter = filterType, Controller = typeof(TController) });
            return this;
        }

        public IEnumerable<FilterReg> GetFilterRegistrations() {
            return FilterList;
        }
    }
}
