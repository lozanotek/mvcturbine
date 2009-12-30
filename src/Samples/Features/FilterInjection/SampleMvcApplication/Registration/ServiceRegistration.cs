namespace MvcTurbine.Samples.FilterInjection.Registration {
    using ComponentModel;
    using Services;
    using Services.Impl;

    public class ServiceRegistration : IServiceRegistration {
        #region IComponentRegistration Members

        public void Register(IServiceLocator locator) {
            locator.Register<IMessageService, MessageService>();
        }

        #endregion
    }
}