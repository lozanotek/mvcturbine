using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc3Host
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Routing;
    using MvcTurbine.Web;
    using MvcTurbine.Web.Controllers;
    using MvcTurbine.Windsor;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : TurbineApplication
    {
        static MvcApplication()
        {
            var locator = new WindsorServiceLocator();
            ServiceLocatorManager.SetLocatorProvider(() => locator);
        }
    }

    public class MvcRouting : IRouteRegistrator
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