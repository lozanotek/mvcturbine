namespace Mvc.Services
{
    using MvcTurbine.ComponentModel;

    /// <summary>
    /// The purpose of this type is to show how you can register types
    /// without knowning (or caring) about the underlying IoC container.  
    /// 
    /// If you care about the specifics of type registration, do not use this approach
    /// instead reference the registration pieces within the <see cref="DefaultMvcApplication"/>.
    /// </summary>
    public class DefaultServiceRegistration : IServiceRegistration
    {
        public void Register(IServiceLocator locator)
        {
            // Simple registration for this service type,
            // if type registration is important to you please 
            // read the above note.
            locator.Register<IMessageService, MessageService>();
        }
    }
}
