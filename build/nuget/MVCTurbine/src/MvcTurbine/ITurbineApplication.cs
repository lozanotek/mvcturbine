namespace MvcTurbine {
    using ComponentModel;

    /// <summary>
    /// Provides the infrastructure for Turbine flow.
    /// </summary>
    public interface ITurbineApplication {

        /// <summary>
        /// Gets or sets the current implementation of <see cref="IServiceLocator"/>
        /// the application instance will use.
        /// </summary>
        IServiceLocator ServiceLocator { get; set; }

        /// <summary>
        /// Gets or sets the current <see cref="IRotorContext"/> for the application instance to use.
        /// </summary>
        IRotorContext CurrentContext { get; set; }

        /// <summary>
        /// Performs any startup processing.
        /// </summary>
        void Startup();

        /// <summary>
        /// Turns the current <see cref="CurrentContext"/>
        /// </summary>
        void TurnRotor();

        /// <summary>
        /// Shuts down the current application.
        /// </summary>
        void Shutdown();
    }
}