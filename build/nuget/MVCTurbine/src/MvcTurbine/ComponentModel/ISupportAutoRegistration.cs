namespace MvcTurbine.ComponentModel {

    /// <summary>
    /// Defines the contract for processing auto-registration within the system.
    /// </summary>
    public interface ISupportAutoRegistration {

        /// <summary>
        /// Passes the current <see cref="AutoRegistrationList"/> that contains all <see cref="ServiceRegistration"/>
        /// to process.
        /// </summary>
        /// <param name="registrationList"></param>
        void AddRegistrations(AutoRegistrationList registrationList);
    }
}