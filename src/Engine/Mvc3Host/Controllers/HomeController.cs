using System.Web.Mvc;

namespace Mvc3Host.Controllers {
    using Mvc3Host.Filters;
    using Mvc3Host.Models;

    public class HomeController : Controller {
        [Foo]
        public ActionResult Index(Foo foo, Bar bar) {
            ViewModel.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}
