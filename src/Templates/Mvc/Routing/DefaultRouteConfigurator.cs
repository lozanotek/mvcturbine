namespace Mvc.Routing
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcTurbine.Routing;

    public class DefaultRouteConfigurator : IRouteConfigurator
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
        }
    }
}
