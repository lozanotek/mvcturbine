namespace MvcTurbine.Samples.FilterInjection.Filters {
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Services;

    public class ReplayAuthAttribute : AuthorizeAttribute {
        public string Message { get; set; }

        // Need to tell Unity we're a dependency and that we
        // need to be resolved accordingly
        [Dependency]
        public IMessageService MessageService { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext) {
            filterContext.Controller.ViewData["ReplayAuthMessage"] = MessageService.ReplayMessage(Message);
        }
    }
}
