namespace MvcTurbine.Poco {
	using System.Web.Mvc;

	public class PocoActionResult : ActionResult {
		public PocoActionResult() {
		}

		public PocoActionResult(object pocoModel) {
			Model = pocoModel;
		}

		public object Model { get; set; }

		public override void ExecuteResult(ControllerContext context) {
			JsonResult result = new JsonResult { Data = Model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			result.ExecuteResult(context);
		}
	}
}