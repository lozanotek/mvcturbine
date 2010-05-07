namespace Mvc
{
    using MvcTurbine.ComponentModel;
    using MvcTurbine.StructureMap;
    using MvcTurbine.Web;
    using Services;
    using StructureMap;

    public class DefaultMvcApplication : TurbineApplication
    {
        //NOTE: You want to hit this piece of code only once.
        static DefaultMvcApplication()
        {
            //TODO: Specify your own service locator here.
            var container = CreateContainer();
            ServiceLocatorManager.SetLocatorProvider(() => new StructureMapServiceLocator(container));
        }

        public static IContainer CreateContainer()
        {
            // Register the types here
            var container = new Container(config => config.For<IMessageService>().Use<MessageService>());
            return container;
        }
    }

    /*
     * If you don't care about how registration is done, or you have some common services 
     * for Blades that provide cross-cutting concerns.
     * 
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
    */
}
