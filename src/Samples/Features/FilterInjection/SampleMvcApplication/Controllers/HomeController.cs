namespace MvcTurbine.Samples.FilterInjection.Controllers {
    using System.Web.Mvc;
    using Filters;

    [HandleError]
    public class HomeController : Controller {
        // This is an injectable action filter that uses the deprecated
        // InjectableFilterAttribute type
        [CustomAction]

        // This action filter uses property injection to have its dependencies
        // injected so it can be easy to use and test.
        [ReplayAction(Message = "I'm an action filter!")]

        // This is an injectable authorization filter that uses the deprecated
        // InjectableFilterAttribute type
        [CustomAuthorization]

        // This authorization filter uses property injection to have its dependencies
        // injected so it can be easy to use and test.
        [ReplayAuth(Message = "I'm an authorization filter!")]

        public ActionResult Index() {
            return View();
        }
    }
}
