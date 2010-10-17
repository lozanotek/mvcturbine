namespace Mvc3Host.Filters {
    using MvcTurbine.Web.Filters;

    public class MyGlobalFilters : GlobalFilterRegistry {
        public MyGlobalFilters() {
            AsGlobal<GlobalFilter>();
        }
    }
}