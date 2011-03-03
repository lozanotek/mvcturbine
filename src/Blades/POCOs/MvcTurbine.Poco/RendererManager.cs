namespace MvcTurbine.Poco {
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Poco.Renderer;

    public class RendererManager : IRendererManager {
        public RendererManager(IServiceLocator serviceLocator, IEnumerable<RendererReg> rendererRegistrations) {
            ServiceLocator = serviceLocator;
            RendererRegistrations = rendererRegistrations;
        }

        public IServiceLocator ServiceLocator { get; private set; }
        public IEnumerable<RendererReg> RendererRegistrations { get; private set; }

        public virtual IRenderer GetRenderer(ControllerContext context, string acceptType) {
            if (RendererRegistrations == null) return null;

            var rendererType = RendererRegistrations
                .Where(reg => reg.AcceptTypes.Contains(acceptType))
                .Select(reg => reg.RendererType)
                .FirstOrDefault();

            if (rendererType == null) {
                return new JsonRenderer();
            }

            return ServiceLocator.Resolve(rendererType) as IRenderer;
        }
    }
}