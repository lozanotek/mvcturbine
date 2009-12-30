namespace MvcTurbine.Samples.FilterInjection.Filters {
    using System;
    using Web.Controllers;

    /// <summary>
    /// Defines the an injectable IActionFilter
    /// </summary>
    public class CustomActionAttribute : InjectableFilterAttribute {
        public override Type FilterType {
            get { return typeof(CustomActionFilter); }
        }
    }
}