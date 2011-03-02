namespace Mvc3Host.Filters {
    using MvcTurbine.Web.Filters;

    public sealed class MyGlobalFilters : GlobalFilterRegistry {
        public MyGlobalFilters() {
            AsGlobal<GlobalFilter>(filter => filter.Value = 10);
        }
    }
}
