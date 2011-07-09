namespace MvcTurbine {
    using System;
    using Blades;
    using ComponentModel;

    /// <summary>
    /// Defines the default flow of a <see cref="ITurbineApplication"/> instance.
    /// </summary>
    public interface IRotorContext : IDisposable {
        /// <summary>
        /// Gets or sets the current implementation of <see cref="IServiceLocator"/>.
        /// </summary>
        IServiceLocator ServiceLocator { get; }

        /// <summary>
        /// Gets or sets the current instance of <see cref="ITurbineApplication"/>.
        /// </summary>
        ITurbineApplication Application { get; }

        /// <summary>
        /// Initializes the current context by auto-registering the default components.
        /// </summary>
        void Initialize(ITurbineApplication application);

        /// <summary>
        /// Executes the current context.
        /// </summary>
        void Turn();

        /// <summary>
        /// Gets the list of components that are to be used for the application.
        /// </summary>
        /// <returns>A list of the components registered with the application.</returns>
        BladeList GetAllBlades();
    }
}