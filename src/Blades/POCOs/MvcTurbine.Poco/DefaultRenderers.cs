namespace MvcTurbine.Poco {
    using MvcTurbine.Poco.Renderer;

    public class DefaultRenderers : RendererRegistry {
        public DefaultRenderers() {
            With<JsonRenderer>().Handle("application/json");
            With<XmlSerializerRenderer>().Handle("application/xml");
        }
    }
}
