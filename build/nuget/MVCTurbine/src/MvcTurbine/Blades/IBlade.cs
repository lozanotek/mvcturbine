namespace MvcTurbine.Blades {
    using System;

    /// <summary>
    /// Defines the contract for all blades (components) to use.
    /// </summary>
    public interface IBlade : IDisposable {
        /// <summary>
        /// Initializes the blade.
        /// </summary>
        /// <param name="context">Current context for the <see cref="Blade"/> instance.</param>
        void Initialize(IRotorContext context);

        /// <summary>
        /// Executes the current component.
        /// </summary>
        void Spin(IRotorContext context);
    }
}