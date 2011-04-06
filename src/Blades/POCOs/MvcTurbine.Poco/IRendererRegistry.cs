namespace MvcTurbine.Poco {
    using System.Collections.Generic;

    public interface IRendererRegistry {
        IEnumerable<RendererReg> GetRenderers();
    }
}