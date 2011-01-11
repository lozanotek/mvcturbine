namespace Mvc3Host.Models {
    using System.Web.Mvc;

    public class PersonBinder : IModelBinder {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            return new Person { Name = "Test" };
        }
    }
}