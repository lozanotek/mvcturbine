namespace MvcTurbine.Web.Filters {
    using System.Collections.Generic;

    ///<summary>
    /// Provides the common contract for providing filter registries into the runtime.
    ///</summary>
    public interface IFilterRegistry {
        /// <summary>
        /// Gets the registered filters (Action,Result,Exception,Authorization).
        /// </summary>
        /// <returns></returns>
        IEnumerable<Filter> GetFilterRegistrations();
    }
}
