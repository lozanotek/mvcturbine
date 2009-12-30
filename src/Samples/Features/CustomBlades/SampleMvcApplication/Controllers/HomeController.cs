namespace MvcTurbine.Samples.CustomBlades.Controllers {
    using System.Web.Mvc;

    [HandleError]
    public class HomeController : Controller {

        public ActionResult Index() {
            ViewData["BeforeDependency"] = ControllerContext.HttpContext.Application["BeforeDependency"];
            ViewData["AfterDependency"] = ControllerContext.HttpContext.Application["AfterDependency"];

            return View();
        }
    }
}