namespace MvcTurbine.Samples.CustomBlades.Registration {
    using Blades;
    using ComponentModel;

    public class BladeRegistrations : IServiceRegistration {
        #region IComponentRegistration Members

        public void Register(IServiceLocator locator) {
            // Register the dependency for the CustomBlade.
            locator.Register<IBladeDependency, BladeDependency>();
        }

        #endregion
    }
}