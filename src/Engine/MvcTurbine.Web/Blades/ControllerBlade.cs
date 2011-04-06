namespace MvcTurbine.Web.Blades {
    using System.Web.Mvc;
    using ComponentModel;
    using Controllers;
    using MvcTurbine.Blades;

    /// <summary>
    /// Blade for all controller related components.
    /// </summary>
    public class ControllerBlade : Blade, ISupportAutoRegistration {
        /// <summary>
        /// Sets the instance of <see cref="IControllerFactory"/> to use.  If one is not registered,
        /// <see cref="IControllerActivator"/> is used.
        /// </summary>
        public override void Spin(IRotorContext context) {
        }

        /// <summary>
        /// Provides the auto-registration for <see cref="IController"/> types.
        /// </summary>
        /// <param name="registrationList"></param>
        public virtual void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(MvcRegistration.RegisterController());
        }
    }
}
