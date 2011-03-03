namespace MvcTurbine.Poco {
    using System.Collections.Generic;
    using MvcTurbine.Blades;
    using MvcTurbine.ComponentModel;

    public class RendererBlade : Blade, ISupportAutoRegistration {
        public override void Spin(IRotorContext context) {
            var serviceLocator = GetServiceLocatorFromContext(context);
            var registries = GetRenderRegistries(serviceLocator);

            if (registries == null) return;

            using (serviceLocator.Batch()) {
                foreach (var registry in registries) {
                    var registrations = registry.GetRenderers();

                    foreach (var registration in registrations) {
                        serviceLocator.Register(registration.RendererType, registration.RendererType);
                    }

                    Renderers.Current.Add(registrations);
                }
            }
        }

        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(Registration.Simple<IRendererRegistry>());
        }

        protected virtual IList<IRendererRegistry> GetRenderRegistries(IServiceLocator locator) {
            try {
                return locator.ResolveServices<IRendererRegistry>();
            }
            catch {
                return null;
            }
        }
    }
}