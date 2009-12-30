namespace Mvc
{
    using MvcTurbine;
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Windsor;

    public class DefaultMvcApplication : TurbineApplication
    {
        //NOTE: You want to hit this piece of code only once.
        static DefaultMvcApplication()
        {
            //TODO: Specify your own service locator here.
            ServiceLocatorManager.SetLocatorProvider(() => new WindsorServiceLocator());
        }
    }
}
