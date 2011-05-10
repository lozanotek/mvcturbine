namespace MvcTurbine.Samples.CustomBlades.Registration {
    using Blades;
    using ComponentModel;

    public class BladeRegistrations : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            // Register the dependency for the CustomBlade.
            locator.Register<IBladeDependency, BladeDependency>();
        }
    }
}