namespace MvcTurbine.Web.Views {
    using System.Web.Mvc;
    using MvcTurbine.ComponentModel;

    public static class HtmlHelperExtensions {
        public static IServiceLocator ServiceLocator(this HtmlHelper helper) {
            return helper.ViewContext.ServiceLocator();
        }

        public static dynamic Service(this HtmlHelper helper) {
            return new DynamicLocator(helper);
        }
    }
}
