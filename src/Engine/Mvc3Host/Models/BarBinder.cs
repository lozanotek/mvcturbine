namespace Mvc3Host.Models {
    using System;
    using System.Web.Mvc;
    using MvcTurbine;
    using MvcTurbine.Web.Models;

    public class BarBinder : IFilterableModelBinder {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            return new Bar { Name = "Bar" };
        }

        public bool SupportsModelType(Type modelType) {
            return modelType.IsType<Bar>();
        }
    }
}