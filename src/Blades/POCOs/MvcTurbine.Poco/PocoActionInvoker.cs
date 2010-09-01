namespace MvcTurbine.Poco {
	using System.Web.Mvc;
	using ComponentModel;
	using Web.Controllers;

	public class PocoActionInvoker : TurbineActionInvoker {
		public PocoActionInvoker(IServiceLocator locator) : base(locator) {
		}

		protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue) {
			if (!(actionReturnValue is ActionResult)) {
				controllerContext.Controller.ViewData.Model = actionReturnValue;
				return new PocoActionResult(actionReturnValue);
			}

			return base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
		}
	}
}