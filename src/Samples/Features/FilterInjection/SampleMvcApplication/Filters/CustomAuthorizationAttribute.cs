namespace MvcTurbine.Samples.FilterInjection.Filters {
    using System;
    using Web.Controllers;

    /// <summary>
    /// Defines the an injectable IAuthorizationFilter
    /// </summary>
    public class CustomAuthorizationAttribute : InjectableFilterAttribute {
        public override Type FilterType {
            get { return typeof(CustomAuthorizationFilter); }
        }
    }
}