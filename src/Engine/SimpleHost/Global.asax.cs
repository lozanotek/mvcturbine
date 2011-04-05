using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleHost
{
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Routing;
    using MvcTurbine.StructureMap;
    using MvcTurbine.Web;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : TurbineApplication
    {
        static MvcApplication()
        {
            ServiceLocatorManager.SetLocatorProvider(() => new StructureMapServiceLocator());
        }

    }

    public class RouteRegistration : IRouteRegistrator
    {
        public void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}