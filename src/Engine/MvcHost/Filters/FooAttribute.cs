namespace Mvc3Host.Filters {
    using System.Web.Mvc;
    using Mvc3Host.Services;

    public class FooAttribute : ActionFilterAttribute {
        public IFooService FooService { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
			var message = (FooService == null) ? "FooService was not injected!" : FooService.GetFoo();
			filterContext.Controller.ViewBag.fooMessage = message;
        }
    }
}