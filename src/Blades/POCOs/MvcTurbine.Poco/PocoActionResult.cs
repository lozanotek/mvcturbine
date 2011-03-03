namespace MvcTurbine.Poco {
    using System;
    using System.Web;
    using System.Web.Mvc;
    using MvcTurbine.ComponentModel;

    public class PocoActionResult : ActionResult {
        private static object _lock = new object();
        private static IRendererManager rendererManager;
        public IServiceLocator ServiceLocator { get; private set; }

        public PocoActionResult(IServiceLocator serviceLocator, object model) {
            ServiceLocator = serviceLocator;
            Model = model;
        }

        public object Model { get; set; }

        public override void ExecuteResult(ControllerContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            var renderer = GetRenderer(context);

            if (renderer == null) {
                throw new Exception("Renderer was not registered");
            }

            response.ContentType = renderer.ContentType;
            renderer.Render(context, Model);
        }

        private IRenderer GetRenderer(ControllerContext context) {
            HttpRequestBase request = context.HttpContext.Request;
            var rendererManager = GetRendererManager();

            var acceptTypes = request.AcceptTypes;

            foreach (var acceptType in acceptTypes) {
                var renderer = rendererManager.GetRenderer(context, acceptType);
                if (renderer == null) continue;

                return renderer;
            }

            return null;
        }

        protected virtual IRendererManager GetRendererManager() {
            if (rendererManager == null) {
                lock (_lock) {
                    if (rendererManager == null) {
                        try {
                            rendererManager = ServiceLocator.Resolve<IRendererManager>();
                        }
                        catch {
                            rendererManager = new RendererManager(ServiceLocator, Renderers.Current);
                        }
                    }
                }
            }

            return rendererManager;
        }
    }
}
