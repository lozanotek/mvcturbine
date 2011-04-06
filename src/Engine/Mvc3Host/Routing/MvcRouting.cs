namespace Mvc3Host.Routing {
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcTurbine.Routing;

    public class MvcRouting : IRouteRegistrator {
        public void Register(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );
        }
    }
}