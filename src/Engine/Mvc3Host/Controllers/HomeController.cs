using System.Web.Mvc;

namespace Mvc3Host.Controllers {
    using Mvc3Host.Filters;
    using Mvc3Host.Models;

    [HandleError]
    public class HomeController : Controller {
        [Foo]
        public ActionResult Index(Person person, Bar bar) {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }
    }
}
