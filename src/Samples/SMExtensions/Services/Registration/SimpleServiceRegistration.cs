namespace Services.Registration {
    using MvcTurbine.ComponentModel;

    public class SimpleServiceRegistration : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            locator.Register<IDateService, DateService>();
        }
    }
}