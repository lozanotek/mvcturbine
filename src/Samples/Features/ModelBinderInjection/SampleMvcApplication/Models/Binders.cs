namespace MvcTurbine.Samples.ModelBinders.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using MvcTurbine.Web.Models;

	public class Binders : ModelBinderRegistry
	{
		public Binders()
		{
			Bind<PersonInputModel, PersonModelBinder>();
		}
	}
}
