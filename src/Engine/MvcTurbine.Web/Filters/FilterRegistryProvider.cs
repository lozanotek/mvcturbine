namespace MvcTurbine.Web.Filters
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ComponentModel;

    public class FilterRegistryProvider : IFilterProvider {
        public IServiceLocator ServiceLocator { get; private set; }

        public FilterRegistryProvider(IServiceLocator serviceLocator) {
            ServiceLocator = serviceLocator;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)  {
            //TODO: implement this piece
            return new List<Filter>();
        }
    }
}
