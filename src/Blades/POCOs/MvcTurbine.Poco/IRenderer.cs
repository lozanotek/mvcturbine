namespace MvcTurbine.Poco {
    using System.Web.Mvc;

    public interface IRenderer {
        string ContentType { get; }
        void Render(ControllerContext context, object model);
    }
}