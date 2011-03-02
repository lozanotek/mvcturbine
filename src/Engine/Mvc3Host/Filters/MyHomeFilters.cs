namespace Mvc3Host.Filters {
    using Mvc3Host.Controllers;
    using MvcTurbine.Web.Filters;

    public sealed class MyHomeFilters : ControllerFilterRegistry<HomeController> {
        public MyHomeFilters() {
            // Controller level filter
            With<Home>(filter => filter.Value = 10);

            // Action level filter (with intellisense)
            ForAction<HomeAction>(c => c.Index(null, null), 
                filter => filter.Name = "First test");

            // (Inferred) Action filter
            ForAction<HomeAction>("about", filter => filter.Name = "Test");
        }
    }
}
