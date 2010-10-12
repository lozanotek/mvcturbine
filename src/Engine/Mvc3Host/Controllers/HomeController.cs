using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc3Host.Controllers
{
    using MvcTurbine;
    using MvcTurbine.Web.Models;

    public class FooRegistry : ModelBinderRegistry
    {
        public FooRegistry()
        {
            Register<Foo, FooBinder>();
        }
    }

    public class Foo
    {
        public string Name { get; set; }
    }

    public class Bar
    {
        public string Name { get; set; }
    }

    public class BarBinder : IFilterableModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return new Bar {Name = "Bar"};
        }

        public bool SupportsModelType(Type modelType)
        {
            return modelType.IsType<Bar>();
        }
    }

    public class FooBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return new Foo {Name = "Foo"};
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index(Foo foo, Bar bar)
        {
            ViewModel.Message = "Welcome to ASP.NET MVC!";

            return View();
        }
    }
}
