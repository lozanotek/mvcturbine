namespace MvcTurbine.Samples.InferredActions.Routing {
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcTurbine.Routing;

    public class RouteConfigurator : IRouteRegistrator {
        #region IRouteConfigurator Members

        public void Register(RouteCollection routes) {
            // Default route registration

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default",
                            "{controller}/{action}/{id}",
                            new { controller = "Home", action = "Index", id = "" });
        }

        #endregion
    }
}