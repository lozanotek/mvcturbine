namespace Mvc3Host.Filters {
    using Mvc3Host.Controllers;
    using MvcTurbine.Web.Filters;

    public sealed class MyHomeFilters : ControllerFilterRegistry<HomeController> {
        public MyHomeFilters() {
            // Controller level filter
            Apply<Home>(filter => filter.Value = 10);
                
            // Action level filter (with intellisense)
            Apply<HomeAction>(filter => filter.Name = "First test")
                .ToAction(c => c.Index(null, null))
         
            // (Inferred) Action filter            
                .ToAction("about");
        }
    }
}
