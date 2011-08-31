namespace MvcTurbine.Web.Views {
    using System.Collections.Generic;

    public interface IViewEngineProvider {
        IList<ViewEngine> GetViewEngineRegistrations();
    }
}