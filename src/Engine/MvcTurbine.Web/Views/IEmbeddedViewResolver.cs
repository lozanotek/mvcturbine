namespace MvcTurbine.Web.Views {
    /// <summary>
    /// Defines the interface for providing embedded view lists to the system.
    /// </summary>
    public interface IEmbeddedViewResolver {
        /// <summary>
        /// Gets a list of embedded views within the system.
        /// </summary>
        /// <returns></returns>
        EmbeddedViewTable GetEmbeddedViews();
    }
}