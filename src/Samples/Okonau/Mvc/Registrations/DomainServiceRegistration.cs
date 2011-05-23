namespace Okonau.Mvc.Registrations {
    using MvcTurbine.ComponentModel;
    using Services;

    /// <summary>
    /// Registration for Domain Services for the application to use.
    /// </summary>
    public class DomainServiceRegistration : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            locator.Register<ITaskService, TaskService>();
        }
    }
}
