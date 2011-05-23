namespace Okonau.Mvc.Routing {
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcTurbine.Routing;

    /// <summary>
    /// Default route registration for the application.
    /// </summary>
    public class DefaultRouteRegistrator : IRouteRegistrator {
        public void Register(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default",
                "{controller}/{action}/{id}",
                new { controller = "Task", action = "Index", id = "" });
        }
    }
}