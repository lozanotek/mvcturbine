namespace MvcTurbine.Samples.ControllerInjection.Routing {
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcTurbine.Routing;

    public class RouteConfigurator : IRouteRegistrator {
        #region IRouteConfigurator Members

        public void Register(RouteCollection routes) {

            // Defines the default routes for the application to use
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default",
                            "{controller}/{action}/{id}",
                            new { controller = "Home", action = "Index", id = "" });
        }

        #endregion
    }
}