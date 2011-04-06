namespace Mvc3Host.Filters {
    using System.Web.Mvc;
    using Mvc3Host.Services;

    public class GlobalFilter : IActionFilter {
        public IFooService Service { get; private set; }

        public int Value { get; set; }

        public GlobalFilter(IFooService service) {
            Service = service;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            filterContext.Controller.ViewBag.globalMessage = 
                string.Format("[global] Message: {0} -- Value: {1}", Service.GetFoo(), Value);
        }
    }
}