namespace MvcTurbine.Samples.FilterInjection.Controllers {
    using System.Web.Mvc;
    using Filters;

    [HandleError]
    public class HomeController : Controller {
        // This is an injectable action filter
        [CustomAction]

        // This is an injectable authorization filter
        [CustomAuthorization]
        public ActionResult Index() {
            return View();
        }
    }
}