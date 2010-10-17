namespace Mvc3Host.Filters {
    using System.Web.Mvc;

    public class HomeFilter : IActionFilter {
        public void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.Controller.ViewModel.homeMessage = "I'm in the home controller";

        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
        }
    }
}