namespace Mvc.Routing {
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcTurbine.Routing;

    public class DefaultRouteRegistrator : IRouteRegistrator {
        public void Register(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "WebForm", action = "Index", id = "" }  // Parameter defaults
            );
        }
    }
}
