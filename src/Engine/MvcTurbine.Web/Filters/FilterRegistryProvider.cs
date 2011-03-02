namespace MvcTurbine.Web.Filters {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Provides the link between the <see cref="IFilterRegistry"/> and the MVC3 runtime.
    /// </summary>
    public class FilterRegistryProvider : IFilterProvider {
        /// <summary>
        /// Gets the associated <see cref="IServiceLocator"/>.
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Gets the list of registrations that were found within the system.
        /// </summary>
        public IList<FilterReg> RegistryList { get; private set; }

        /// <summary>
        /// Public default constructor
        /// </summary>
        /// <param name="serviceLocator"></param>
        /// <param name="registryList"></param>
        public FilterRegistryProvider(IServiceLocator serviceLocator, IList<FilterReg> registryList) {
            ServiceLocator = serviceLocator;
            RegistryList = registryList;
        }

        /// <summary>
        /// Gets all the filters that were registered with the <see cref="IFilterRegistry"/> instances.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor) {
            var globalList = GetGlobalFilters();
            var controllerList = GetControllerFilters(controllerContext);
            var actionList = GetActionFilters(actionDescriptor);

            return globalList.Concat(controllerList).Concat(actionList);
        }

        /// <summary>
        /// Gets all the action filters associated with the request.
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        private IEnumerable<Filter> GetActionFilters(ActionDescriptor actionDescriptor) {
            var controller = actionDescriptor.ControllerDescriptor.ControllerType;
            return RegistryList.OfType<ActionFilterReg>()
                .Where(reg => controller == reg.Controller)
                .Where(reg => string.Equals(reg.Action, actionDescriptor.ActionName, StringComparison.CurrentCultureIgnoreCase))
                .Select(ToMvcFilter);
        }

        /// <summary>
        /// Gets all the controller filters asssociated with the request.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        private IEnumerable<Filter> GetControllerFilters(ControllerContext controllerContext) {
            return RegistryList.OfType<ControllerFilterReg>()
                .Where(reg => controllerContext.Controller.IsType(reg.Controller))
                .Select(ToMvcFilter);
        }

        /// <summary>
        /// Gets all the global filters associated with the request.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Filter> GetGlobalFilters() {
            return RegistryList.OfType<GlobalFilterReg>().Select(ToMvcFilter);
        }

        /// <summary>
        /// Creates the instance of the filter (global and controller).
        /// </summary>
        /// <param name="filterReg"></param>
        /// <returns></returns>
        public virtual Filter ToMvcFilter(FilterReg filterReg) {
            var instance = ServiceLocator.Resolve(filterReg.Filter);

            if (filterReg.ModelInitializer != null) {
                filterReg.ModelInitializer(instance);
            }

            return new Filter(instance, filterReg.Scope, filterReg.Order);
        }
    }
}
