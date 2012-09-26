namespace Mvc3Host.Filters {
    using System.Web.Mvc;

    public class HomeAction : IActionFilter {
        public string Name { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            var msg = "I'm in the home controller in action " +
                   filterContext.ActionDescriptor.ActionName;

            if (!string.IsNullOrEmpty(Name)) {
                msg += " and my name is " + Name;
            }

            filterContext.Controller.ViewBag.homeActionMessage = msg;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
        }
    }
}
