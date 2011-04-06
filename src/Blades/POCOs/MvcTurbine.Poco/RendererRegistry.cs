namespace MvcTurbine.Poco {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class RendererRegistry : IRendererRegistry {
        protected RendererRegistry() {
            Renderers = new Dictionary<RendererReg, AcceptTypeExpression>();
        }

        protected IDictionary<RendererReg, AcceptTypeExpression> Renderers { get; private set; }

        public AcceptTypeExpression With<TRenderer>()
            where TRenderer : IRenderer {

            var rendererType = typeof(TRenderer);

            RendererReg renderReg = Renderers
                .Where(renderer => renderer.Key.RendererType == rendererType)
                .Select(renderer => renderer.Key)
                .FirstOrDefault();

            AcceptTypeExpression typeExpression;

            if (renderReg == null) {
                renderReg = new RendererReg { RendererType = rendererType };
                typeExpression = new AcceptTypeExpression(renderReg);

                Renderers.Add(renderReg, typeExpression);
            }
            else {
                typeExpression = Renderers[renderReg];
            }

            return typeExpression;
        }

        public IEnumerable<RendererReg> GetRenderers() {
            return Renderers.Keys;
        }
    }

    public class AcceptTypeExpression {
        public RendererReg Renderer { get; private set; }

        public AcceptTypeExpression(RendererReg renderer) {
            Renderer = renderer;
        }

        public AcceptTypeExpression Handle(string acceptType) {
            Renderer.AcceptTypes.Add(acceptType);
            return this;
        }
    }
}