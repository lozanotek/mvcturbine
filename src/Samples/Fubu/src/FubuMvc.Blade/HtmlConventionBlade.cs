namespace FubuMvc.Blades.UI {
    using FubuMVC.UI;
    using FubuMVC.UI.Tags;
    using MvcTurbine;
    using MvcTurbine.Blades;
    using MvcTurbine.ComponentModel;

    public class HtmlConventionBlade : Blade, ISupportAutoRegistration {
        public void AddRegistrations(AutoRegistrationList registrationList) {
            // Tell the system to scan and auto-register all the HtmlConventionRegistry types
            registrationList.Add(Registration.Simple<HtmlConventionRegistry>());
        }

        public override void Spin(IRotorContext context) {
            IServiceLocator locator = context.ServiceLocator;

            var library = locator.Resolve<TagProfileLibrary>();

            // Get all the registered HTML conventions
            var conventions = locator.ResolveServices<HtmlConventionRegistry>();

            foreach (HtmlConventionRegistry convention in conventions) {
                library.ImportRegistry(convention);
            }
        }
    }
}
