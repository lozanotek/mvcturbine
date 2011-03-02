namespace Mvc3Host.Services {
    using Mvc3Host.Models;
    using MvcTurbine.ComponentModel;

    public class ServiceRegistry : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            locator.Register<IFooService, FooService>("Foo");

            locator.Register(() => new Person {Name = "Bob Smith"});
        }
    }
}