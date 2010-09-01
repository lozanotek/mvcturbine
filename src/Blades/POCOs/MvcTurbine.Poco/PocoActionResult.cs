namespace MvcTurbine.Poco {
	using System;
	using System.Web;
	using System.Web.Mvc;
	using Newtonsoft.Json;

	public class PocoActionResult : ActionResult {
		public PocoActionResult() {
		}

		public PocoActionResult(object pocoModel) {
			Model = pocoModel;
		}

		public object Model { get; set; }

		public override void ExecuteResult(ControllerContext context) {
			if (context == null) {
				throw new ArgumentNullException("context");
			}

			HttpResponseBase response = context.HttpContext.Response;
			response.ContentType = "application/json";

			var writer = new JsonTextWriter(response.Output) { Formatting = Formatting.None };
			JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings());
			serializer.Serialize(writer, Model);

			writer.Flush();
		}
	}
}
