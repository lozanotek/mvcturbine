namespace Mvc3Host.Filters {
    using Mvc3Host.Controllers;
    using MvcTurbine.Web.Filters;

    public class HomeFilterRegistry : ControllerFilterRegistry<HomeController> {
        public HomeFilterRegistry() {
            // Controller level filter
            With<HomeFilter>();

            // Action level filter (with intellisense)
            ForAction<HomeActionFilter>(c => c.Index(null, null));

            // (Inferred) Action filter
            ForAction<HomeActionFilter>("about");
        }
    }
}