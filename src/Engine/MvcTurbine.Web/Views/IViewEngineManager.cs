namespace MvcTurbine.Web.Views {
    /// <summary>
    /// Defines the common entry point for view engine management.
    /// </summary>
    public interface IViewEngineManager {
        /// <summary>
        /// Registers all view engines with the ViewEngine collection.
        /// </summary>
        void RegisterEngines();
    }
}