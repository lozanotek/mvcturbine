namespace MvcTurbine.Samples.ExtensionMethods.Html {
    using System.Web.Mvc;
    using Services;
    using Web.Views;

    public static class ContentHelper {
        public static string Content(this HtmlHelper helper) {
            // This class shows how you can resolve items from the 
            // underlying container via the extension method
            var serviceLocator = helper.ViewContext.ServiceLocator();
            var messagService = serviceLocator.Resolve<IMessageService>();

            return messagService.GetContent();
        }
    }
}
