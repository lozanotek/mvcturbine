using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcTurbine.Web.Filters;

namespace MvcTurbine.Samples.LoggingBlade.Web.Filters
{
	public class GlobalFilters : GlobalFilterRegistry
	{
		public GlobalFilters()
		{
			AsGlobal<GlobalLoggingFilter>();
		}
	}
}
