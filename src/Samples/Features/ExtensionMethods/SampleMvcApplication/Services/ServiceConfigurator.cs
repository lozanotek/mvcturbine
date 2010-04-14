namespace MvcTurbine.Samples.ExtensionMethods.Services {
    using ComponentModel;
    using global::Ninject;
    using Impl;

    public class ServiceConfigurator : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            // Get the Ninject IKernel that's associated with the application
            var ninjectKernel = locator.GetUnderlyingContainer<IKernel>();

            // Use the power of Ninject to the work for us.
            ninjectKernel.Bind<IMessageService>()
                .To<MessageService>()
                .InSingletonScope();
        }
    }
}
