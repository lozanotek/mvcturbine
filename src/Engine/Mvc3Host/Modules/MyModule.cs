namespace Mvc3Host.Modules {
	using System;
	using System.Web;
	using Mvc3Host.Services;

	public class MyModule : IHttpModule {
		public IFooService Service { get; set; }

		public MyModule(IFooService service) {
			Service = service;
		}

		public void Init(HttpApplication context) {
			context.BeginRequest += new EventHandler(context_BeginRequest);
		}

		private void context_BeginRequest(object sender, EventArgs e) {
			HttpContext.Current.Items["Value"] = Service.GetFoo();
		}

		public void Dispose() { }
	}
}
