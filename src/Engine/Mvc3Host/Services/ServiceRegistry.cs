namespace Mvc3Host.Services {
    using MvcTurbine.ComponentModel;

    public class ServiceRegistry : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            locator.Register<IFooService, FooService>("Foo");
        }
    }
}