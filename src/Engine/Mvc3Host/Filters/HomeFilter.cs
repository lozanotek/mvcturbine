namespace Mvc3Host.Filters {
    using System.Web.Mvc;

    public class HomeFilter : IActionFilter {
        public void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.Controller.ViewBag.homeMessage = "I'm in the home controller";

        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
        }
    }
}