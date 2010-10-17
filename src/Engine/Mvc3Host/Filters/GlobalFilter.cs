namespace Mvc3Host.Filters {
    using System.Web.Mvc;
    using Mvc3Host.Controllers;
    using Mvc3Host.Services;

    public class GlobalFilter : IActionFilter {
        public IFooService Service { get; private set; }

        public GlobalFilter(IFooService service) {
            Service = service;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.Controller.ViewModel.executingMessage = Service.GetFoo();

        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            filterContext.Controller.ViewModel.executedMessage = Service.GetFoo();
        }
    }
}