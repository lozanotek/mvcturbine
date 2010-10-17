namespace Mvc3Host.Filters {
    using Mvc3Host.Controllers;
    using MvcTurbine.Web.Filters;

    public class HomeFilters : ControllerFilterRegistry<HomeController> {
        public HomeFilters() {
            With<HomeFilter>()
                .ForAction<HomeActionFilter>(c => c.Index(null, null));
        }
    }
}