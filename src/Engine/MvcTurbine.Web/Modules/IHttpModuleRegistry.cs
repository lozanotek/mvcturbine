namespace MvcTurbine.Web.Modules {
    using System.Collections.Generic;

    public interface IHttpModuleRegistry {
        IEnumerable<HttpModule> GetModuleRegistrations();
    }
}
