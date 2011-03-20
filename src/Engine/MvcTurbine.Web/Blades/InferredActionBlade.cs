namespace MvcTurbine.Web.Blades {
    using System.Collections.Generic;
    using MvcTurbine.Blades;
    using ComponentModel;
    using Controllers;

    /// <summary>
    /// Blade for alll inferred action components.
    /// </summary>
    public class InferredActionBlade : Blade, ISupportAutoRegistration {
        public override void Spin(IRotorContext context) {
            // Get the current IServiceLocator
            var serviceLocator = GetServiceLocatorFromContext(context);
            var actionRegistries = GetActionRegistries(serviceLocator);

            if (actionRegistries == null) return;

            foreach (var actionRegistry in actionRegistries) {
                var registrations = actionRegistry.GetActionRegistrations();
                InferredActions.Current.AddRegistrations(registrations);
            }
        }

        protected virtual IList<IInferredActionRegistry> GetActionRegistries(IServiceLocator serviceLocator) {
            try {
                return serviceLocator.ResolveServices<IInferredActionRegistry>();
            }
            catch {
                return null;
            }
        }

        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(Registration.Simple<IInferredActionRegistry>());
        }
    }
}