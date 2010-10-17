namespace Mvc3Host.Models {
    using System.Web.Mvc;

    public class FooBinder : IModelBinder {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            return new Foo { Name = "Foo" };
        }
    }
}