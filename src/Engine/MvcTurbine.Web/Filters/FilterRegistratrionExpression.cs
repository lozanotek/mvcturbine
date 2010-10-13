namespace MvcTurbine.Web.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    ///<summary>
    /// Expression for the registration of <see cref="IModelBinder"/> within the system.
    ///</summary>
    public class FilterRegistratrionExpression {
        protected IList<FilterReg> FilterList { get; set; }

        public FilterRegistratrionExpression(IList<FilterReg> filterList) {
            FilterList = filterList ?? new List<FilterReg>();
        }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public FilterRegistratrionExpression ForController<TController, TFilter>()
            where TFilter : IActionFilter, IExceptionFilter, IResultFilter, IMvcFilter, IAuthorizationFilter
            where TController : IController
        {
            return ForController(typeof(TController), typeof(TFilter));
        }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public FilterRegistratrionExpression ForController(Type controllerType, Type filterType) {
            if(!filterType.IsMvcFilter()) {
                throw new ArgumentException("Specified argument is not an MVC filter!", "filterType");
            }

            if(!controllerType.IsController()) {
                throw new ArgumentException("Specified argument is not an MVC controller!", "controllerType");
            }

            FilterList.Add(new FilterReg {FilterType = filterType, Scope = FilterScope.Controller});
            return this;
        }


        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public FilterRegistratrionExpression Global<TFilter>()
            where TFilter : IActionFilter, IExceptionFilter, IResultFilter, IMvcFilter, IAuthorizationFilter {
            return Global(typeof(TFilter));
        }

        /// <summary>
        /// Registers a Global Filter with the system.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <returns></returns>
        public FilterRegistratrionExpression Global(Type filterType) {
            FilterList.Add(new FilterReg {FilterType = filterType, Scope = FilterScope.Global});
            return this;
        }


    }
}