namespace Mvc
{
    using Microsoft.Practices.Unity;
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Unity;
    using MvcTurbine.Web;
    using Services;

    public class DefaultMvcApplication : TurbineApplication
    {
        //NOTE: You want to hit this piece of code only once.
        static DefaultMvcApplication()
        {
            ServiceLocatorManager.SetLocatorProvider(() => new UnityServiceLocator(CreateContainer()));
        }

         static IUnityContainer CreateContainer() 
         {
             var container = new UnityContainer();
             container.RegisterType<IMessageService, MessageService>();

             return container;
         }
    }
}
