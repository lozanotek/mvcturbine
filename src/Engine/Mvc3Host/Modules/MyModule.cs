namespace Mvc3Host.Modules {
	using System;
	using System.Web;
	using Mvc3Host.Services;
	using MvcTurbine.Web.Modules;

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

    public sealed class MyModuleRegistry : HttpModuleRegistry {
        public MyModuleRegistry() {
            // This will add this module
            Add<MyModule>()

            // Will add same module with this specified
            .Add<MyModule>("anotherModule")
            // Will add yet again with different name
            .Add<MyModule>("yetAnotherModule")

            // Will remove the origin module.
            .Remove<MyModule>();
        }
    }
}
