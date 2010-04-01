namespace MvcTurbine.Samples.LoggingBlade {
    using System.Web.Mvc;
    using ComponentModel;

    public class LogRegistration : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            locator.Register<IActionFilter, GlobalLoggingFilter>();
            locator.Register<IResultFilter, GlobalLoggingFilter>();
            locator.Register<IExceptionFilter, GlobalLoggingFilter>();
        }
    }
}