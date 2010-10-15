using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc3Host.Controllers {
    using MvcTurbine;
    using MvcTurbine.ComponentModel;
    using MvcTurbine.Web.Filters;
    using MvcTurbine.Web.Models;

    public class FooRegistry : ModelBinderRegistry {
        public FooRegistry() {
            Bind<Foo, FooBinder>();
        }
    }

    public class Foo {
        public string Name { get; set; }
    }

    public class Bar {
        public string Name { get; set; }
    }

    public class BarBinder : IFilterableModelBinder {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            return new Bar { Name = "Bar" };
        }

        public bool SupportsModelType(Type modelType) {
            return modelType.IsType<Bar>();
        }
    }

    public class FooBinder : IModelBinder {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            return new Foo { Name = "Foo" };
        }
    }

    public class FooAttribute : ActionFilterAttribute {
        public IFooService FooService { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            filterContext.Controller.ViewData["fooMessage"] = FooService.GetFoo();
        }
    }

    public interface IFooService {
        string GetFoo();
    }

    public class FooService : IFooService {
        public string GetFoo() {
            return "Got foo??";
        }
    }

    public class ServiceRegistry : IServiceRegistration {
        public void Register(IServiceLocator locator) {
            locator.Register<IFooService, FooService>();
        }
    }

    public class GlobalFilter : IActionFilter {
        public IFooService Service { get; private set; }

        public GlobalFilter(IFooService service) {
            Service = service;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.Controller.ViewModel.executingMessage = Service.GetFoo();

        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            filterContext.Controller.ViewModel.executedMessage = Service.GetFoo();
        }
    }

    public class MyGlobalFilters : GlobalFilterRegistry {
        public MyGlobalFilters() {
            AsGlobal<GlobalFilter>();
        }
    }

    public class HomeFilter : IActionFilter {
        public void OnActionExecuting(ActionExecutingContext filterContext) {
            filterContext.Controller.ViewModel.homeMessage = "I'm in the home controller";

        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
        }
    }

    public class HomeActionFilter : IActionFilter {
        public void OnActionExecuting(ActionExecutingContext filterContext) {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            filterContext.Controller.ViewModel.homeActionMessage = "I'm in the home controller in action " + filterContext.ActionDescriptor.ActionName;
        }
    }

    public class HomeFilters : ControllerFilterRegistry<HomeController> {
        public HomeFilters() {
            With<HomeFilter>()
            .ForAction<HomeActionFilter>(c => c.Index(null, null));
        }
    }

    public class HomeController : Controller {
        [Foo]
        public ActionResult Index(Foo foo, Bar bar) {
            ViewModel.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}
