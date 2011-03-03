namespace MvcTurbine.Poco {
    using System.Web.Mvc;

    public interface IRendererManager {
        IRenderer GetRenderer(ControllerContext context, string acceptType);
    }
}