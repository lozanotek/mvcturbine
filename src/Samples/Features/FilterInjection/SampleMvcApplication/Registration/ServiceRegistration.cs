namespace MvcTurbine.Samples.FilterInjection.Registration {
    using System.Web.Mvc;
    using ComponentModel;
    using Filters;
    using Services;
    using Services.Impl;

    public class ServiceRegistration : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            locator.Register<IMessageService, MessageService>();
        }
    }
}
