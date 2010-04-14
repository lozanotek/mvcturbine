namespace MvcTurbine.Samples.FilterInjection.Filters {
    using System.Web.Mvc;
    using Services;

    public class GlobalFilter : IActionFilter {
        public IMessageService MessageService { get; private set; }

        public GlobalFilter(IMessageService messageService) {
            MessageService = messageService;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            string message = "I'm a global filter and this is action '" + 
                filterContext.ActionDescriptor.ActionName + "'!!";
            filterContext.Controller.ViewData["GlobalActionMessage"] = MessageService.ReplayMessage(message);
        }
    }
}
