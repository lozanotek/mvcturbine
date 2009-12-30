namespace MvcTurbine.Samples.FilterInjection.Filters {
    using System.Web.Mvc;
    using Services;

    public class CustomActionFilter : BaseFilter, IActionFilter {
        public CustomActionFilter(IMessageService service)
            : base(service) {
        }

        #region IActionFilter Members

        public void OnActionExecuting(ActionExecutingContext filterContext) {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            filterContext.Controller.ViewData["ActionMessage"] = GetFilterMessage();
        }


        #endregion
    }
}