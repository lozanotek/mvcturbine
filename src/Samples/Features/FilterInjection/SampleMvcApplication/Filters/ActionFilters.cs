using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MvcTurbine.Samples.FilterInjection.Controllers;
using MvcTurbine.Web.Filters;

namespace MvcTurbine.Samples.FilterInjection.Filters
{
	public class ActionFilters : ControllerFilterRegistry<HomeController>
	{
		public ActionFilters()
		{
			// This is an injectable action filter that uses the deprecated
			// InjectableFilterAttribute type
			Apply<CustomActionFilter>()
				.ToAction(controller => controller.Index());

			// This is an injectable authorization filter that uses the deprecated
			// InjectableFilterAttribute type
			Apply<CustomAuthorizationFilter>()
				.ToAction("index");
		}
	}

	public class GlobalFilters : GlobalFilterRegistry
	{
		public GlobalFilters()
		{
			AsGlobal<GlobalFilter>();
		}
	}
}
