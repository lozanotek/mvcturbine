namespace Mvc3Host.Models {
    using System.Web.Mvc;
    using Mvc3Host.Services;

    public class PersonBinder : IModelBinder {
        public IFooService Service { get; private set; }

        public PersonBinder(IFooService service) {
            Service = service;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            return new Person { Name = Service.GetFoo() };
        }
    }
}