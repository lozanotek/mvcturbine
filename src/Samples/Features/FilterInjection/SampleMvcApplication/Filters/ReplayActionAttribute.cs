namespace MvcTurbine.Samples.FilterInjection.Filters {
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Services;

    public class ReplayActionAttribute : ActionFilterAttribute {
        public string Message { get; set; }

        // Need to tell Unity we're a dependency and that we
        // need to be resolved accordingly
        [Dependency]
        public IMessageService MessageService { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            filterContext.Controller.ViewData["ReplayActionMessage"] = MessageService.ReplayMessage(Message);
        }
    }
}
