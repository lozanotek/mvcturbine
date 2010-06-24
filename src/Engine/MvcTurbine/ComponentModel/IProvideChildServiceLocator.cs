namespace MvcTurbine.ComponentModel {
    /// <summary>
    /// Defines an interface to provide a child <see cref="IServiceLocator"/> to use
    /// within context.
    /// </summary>
    public interface IProvideChildServiceLocator {

        /// <summary>
        /// Get the child <see cref="IServiceLocator"/> to use within context.
        /// </summary>
        /// <returns></returns>
        IServiceLocator ChildServiceLocator();
    }
}
