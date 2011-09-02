namespace MvcTurbine.Web.Views {
    using System.Collections.Generic;

    /// <summary>
    /// Common interface for providing registration information for View Engines.
    /// </summary>
    public interface IViewEngineProvider {
        /// <summary>
        /// Gets the list of <see cref="ViewEngine"/> registrations to process.
        /// </summary>
        /// <returns></returns>
        IList<ViewEngine> GetViewEngineRegistrations();
    }
}