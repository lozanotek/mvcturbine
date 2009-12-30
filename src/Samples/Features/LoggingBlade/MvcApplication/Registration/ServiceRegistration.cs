namespace MvcTurbine.Samples.LoggingBlade.Web.Registration {
    using ComponentModel;
    using Services;
    using Services.Impl;

    public class ServiceRegistration : IServiceRegistration {
        #region IComponentRegistration Members

        public void Register(IServiceLocator locator) {

            // Specify the implementation for each of the services that will
            // be used by the controllers via constructor injection.

            locator.Register<IMessageService, MessageService>();
        }

        #endregion
    }
}