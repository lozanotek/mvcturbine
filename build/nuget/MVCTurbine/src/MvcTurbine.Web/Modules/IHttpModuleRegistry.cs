namespace MvcTurbine.Web.Modules {
    using System.Collections.Generic;

    /// <summary>
    /// Defines the contract for acquiring all <see cref="HttpModule"/> registrations.
    /// </summary>
    public interface IHttpModuleRegistry {
        /// <summary>
        /// Gets a list of all the <see cref="HttpModule"/> registrations.
        /// </summary>
        /// <returns></returns>
        IEnumerable<HttpModule> GetModuleRegistrations();
    }
}
