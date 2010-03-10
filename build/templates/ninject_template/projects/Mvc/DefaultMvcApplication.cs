namespace Mvc
{
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Ninject;
    using MvcTurbine.Web;
    using Ninject;
    
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

            // Add your type registration here, otherwise if you don't care
            // about how registration is done, check out the 
            // Services\DefaultServiceRegistration.cs file for more information.

            return kernel;
        }
    }
}
