namespace MvcTurbine.ComponentModel {
    /// <summary>
    /// Provides a simple way register components within your application.
    /// </summary>
    public interface IServiceRegistration {

        /// <summary>
        /// Registers the components with the specified <see cref="IServiceLocator"/> instance.
        /// </summary>
        /// <param name="locator">Instance of <see cref="IServiceLocator"/> to use.</param>
        void Register(IServiceLocator locator);
    }
}