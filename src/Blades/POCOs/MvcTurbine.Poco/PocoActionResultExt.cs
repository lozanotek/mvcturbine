namespace MvcTurbine.Poco {
	using System.Web.Mvc;

	public static class PocoActionResultExt {
		public static object GetModel(this ActionResult result) {
			return GetModel<object>(result);
		}

		public static TModel GetModel<TModel>(this ActionResult result) {
			var pocoResult = result as PocoActionResult;
			if (pocoResult == null) {
				return default(TModel);
			}

			return (TModel)pocoResult.Model;
		}
	}
}