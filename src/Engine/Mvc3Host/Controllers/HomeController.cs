using System.Web.Mvc;

namespace Mvc3Host.Controllers {
    using System;
    using System.Web;
    using Mvc3Host.Filters;
    using Mvc3Host.Models;
    using Mvc3Host.Services;

    public class MyModule : IHttpModule {
        public IFooService Service { get; set; }

        public MyModule(IFooService service) {
            Service = service;
        }

        public void Init(HttpApplication context) {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        private void context_BeginRequest(object sender, EventArgs e) {
            HttpContext.Current.Items["Value"] = Service.GetFoo();
        }

        public void Dispose() {
        }
    }

    [HandleError]
    public class HomeController : Controller {
        [Foo]
        public ActionResult Index(Person person, Bar bar) {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }
    }
}
