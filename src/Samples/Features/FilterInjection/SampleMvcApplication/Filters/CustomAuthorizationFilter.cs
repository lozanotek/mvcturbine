namespace MvcTurbine.Samples.FilterInjection.Filters {
    using System.Web.Mvc;
    using Services;

    public class CustomAuthorizationFilter : BaseFilter, IAuthorizationFilter {
        public CustomAuthorizationFilter(IMessageService service)
            : base(service) {
        }

        public void OnAuthorization(AuthorizationContext filterContext) {
            filterContext.Controller.ViewData["AuthMessage"] = GetFilterMessage();
        }
    }
}