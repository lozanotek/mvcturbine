namespace MvcTurbine.Poco {
	using System.Web.Mvc;
	using ComponentModel;
	using Web.Controllers;

	public class PocoActionInvoker : TurbineActionInvoker {
        public IServiceLocator ServiceLocator { get; private set; }

	    public PocoActionInvoker(IServiceLocator locator) : base(null)
		{
		    ServiceLocator = locator;
		}

	    protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue) {
			if (!(actionReturnValue is ActionResult)) {
				controllerContext.Controller.ViewData.Model = actionReturnValue;
				return new PocoActionResult(ServiceLocator, actionReturnValue);
			}

			return base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
		}
	}
}