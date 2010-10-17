namespace Mvc3Host.Filters {
    using System.Web.Mvc;

    public class HomeActionFilter : IActionFilter {
        public void OnActionExecuting(ActionExecutingContext filterContext) {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            filterContext.Controller.ViewModel.homeActionMessage = "I'm in the home controller in action " + filterContext.ActionDescriptor.ActionName;
        }
    }
}