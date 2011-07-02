namespace Mvc3Host.Controllers {
	using System;
	using System.Web;
	using System.Web.Mvc;

	using Mvc3Host.Filters;
	using Mvc3Host.Models;
	using Mvc3Host.Services;
	using MvcTurbine.Web.Modules;

    [HandleError]
	public class HomeController : Controller {
		[Foo]
		public ActionResult Index(Person person, Bar bar)
		{
		    var moduleCount = this.HttpContext.ApplicationInstance.DynamicModules();
			ViewBag.Message = "Welcome to ASP.NET MVC!";

			return View();
		}
	}
}
