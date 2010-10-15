namespace MvcTurbine.Web.Filters {
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ComponentModel;

    public class FilterRegistryProvider : IFilterProvider {
        public IServiceLocator ServiceLocator { get; private set; }
        public IList<FilterReg> RegistryList { get; private set; }

        public FilterRegistryProvider(IServiceLocator serviceLocator, IList<FilterReg> registryList) {
            ServiceLocator = serviceLocator;
            RegistryList = registryList;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor) {
            var globalList = GetGlobalFilters();
            var controllerList = GetControllerFilters(controllerContext);
            var actionList = GetActionFilters(actionDescriptor);

            return globalList.Concat(controllerList).Concat(actionList);
        }

        private IEnumerable<Filter> GetActionFilters(ActionDescriptor actionDescriptor) {
            var controller = actionDescriptor.ControllerDescriptor.ControllerType;
            return RegistryList.OfType<ActionFilterReg>()
                .Where(reg => controller == reg.Controller)
                .Where(reg => reg.Action == actionDescriptor.ActionName)
                .Select(ToMvcFilter);
        }

        private IEnumerable<Filter> GetControllerFilters(ControllerContext controllerContext) {
            return RegistryList.OfType<ControllerFilterReg>()
                .Where(reg => controllerContext.Controller.IsType(reg.Controller))
                .Select(ToMvcFilter);
        }

        protected virtual IEnumerable<Filter> GetGlobalFilters() {
            return RegistryList.OfType<GlobalFilterReg>().Select(ToMvcFilter);
        }

        public virtual Filter ToMvcFilter(FilterReg filter) {
            var instance = ServiceLocator.Resolve(filter.Filter);
            return new Filter(instance, filter.Scope, filter.Order);
        }
    }
}
