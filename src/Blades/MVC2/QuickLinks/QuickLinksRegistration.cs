using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.PortableAreas;

namespace QuickLinks
{
    public class QuickLinksRegistration : PortableAreaRegistration
    {
        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute("ResourceRoute", "quicklinks/resource/{resourceName}",
                new { controller = "EmbeddedResource", action = "Index" },
                new string[] { "MvcContrib.PortableAreas" });

            context.MapRoute("ResourceImageRoute", "quicklinks/images/{resourceName}",
                new { controller = "EmbeddedResource", action = "Index", resourcePath = "images" },
                new string[] { "MvcContrib.PortableAreas" });

            context.MapRoute("quicklink", "quicklinks/{controller}/{action}",
			    new {controller = "links", action = "index"});

            this.RegisterAreaEmbeddedResources();
        }

        public override string AreaName
        {
            get
            {
                return "QuickLinks";
            }
        }
    }
}
