namespace MvcTurbine.Poco.Renderer {
    using System.Web.Mvc;
    using System.Xml.Serialization;

    public class XmlSerializerRenderer : IRenderer {
        public string ContentType {
            get { return "application/xml"; }
        }

        public void Render(ControllerContext context, object model) {
            var response = context.HttpContext.Response;
            var serializer = new XmlSerializer(model.GetType());

            serializer.Serialize(response.OutputStream, model);
        }
    }
}