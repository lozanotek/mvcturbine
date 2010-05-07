namespace FubuMvc.Blades.UI {
    using FubuMVC.UI;
    using FubuMVC.UI.Configuration;
    using FubuMVC.UI.Tags;
    using MvcTurbine.ComponentModel;

    public class SupportingServiceRegistration : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            // Register the common Fubu UI pieces
            locator.Register<IElementNamingConvention, DefaultElementNamingConvention>();
            locator.Register<Stringifier, Stringifier>();
            locator.Register(typeof (ITagGenerator<>), typeof (TagGenerator<>));

            // Wire up the proxy for MS CSL -> MVC Turbine CSL
            locator.Register<Microsoft.Practices.ServiceLocation.IServiceLocator, ServiceLocatorProxy>();

            // Wire up an instance of the TagProfileLibrary for the system to use
            locator.Register(new TagProfileLibrary());
        }
    }
}
