namespace Mvc
{
    using MvcTurbine.ComponentModel;
    using MvcTurbine.StructureMap;
    using MvcTurbine.Web;

    public class DefaultMvcApplication : TurbineApplication
    {
        //NOTE: You want to hit this piece of code only once.
        static DefaultMvcApplication()
        {
            //TODO: Specify your own service locator here.
            ServiceLocatorManager.SetLocatorProvider(() => new StructureMapServiceLocator());
        }
    }
}
