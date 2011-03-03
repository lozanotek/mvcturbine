namespace MvcTurbine.Poco.Renderer {
    using System.Web.Mvc;
    using Newtonsoft.Json;

    public class JsonRenderer : IRenderer {
        public string ContentType {
            get { return "application/json"; }
        }

        public void Render(ControllerContext context, object model) {
            var response = context.HttpContext.Response;
            var writer = new JsonTextWriter(response.Output) { Formatting = Formatting.None };

            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings());
            serializer.Serialize(writer, model);

            writer.Flush();
        }
    }
}