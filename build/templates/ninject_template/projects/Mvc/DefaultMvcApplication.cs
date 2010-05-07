namespace Mvc
{
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Ninject;
    using MvcTurbine.Web;
    using Ninject;
    using Services;

    public class DefaultMvcApplication : TurbineApplication
    {
        //NOTE: You want to hit this piece of code only once.
        static DefaultMvcApplication()
        {
            // Initialize the Ninject Kernel
            IKernel kernel = InitializeNinject();

            // Tell the MVC Turbine runtime to use the initialized kernel
            ServiceLocatorManager.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
        }

        /// <summary>
        /// Create a new instance of <see cref="IKernel"/> with the necessary types registered.
        /// </summary>
        /// <returns></returns>
        static IKernel InitializeNinject()
        {
            IKernel kernel = new StandardKernel();

            // Add your type registration here
            kernel.Bind<IMessageService>().To<MessageService>();

            return kernel;
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
