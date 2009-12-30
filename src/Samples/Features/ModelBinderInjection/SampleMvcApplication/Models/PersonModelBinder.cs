namespace MvcTurbine.Samples.ModelBinders.Models {
    using System;
    using System.Web.Mvc;
    using Web.Models;

    public class PersonModelBinder : IFilterableModelBinder {
        public PersonModelBinder(IModelValidator validator) {
            Validator = validator;
        }

        public IModelValidator Validator { get; private set; }

        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            // Creates the person from the input form values
            PersonInputModel inputModel = CreatePersonInput(controllerContext);

            // Use the injected model validator to check if the values are proper
            if (!Validator.IsValid(inputModel)) {
                bindingContext.ModelState.AddModelError("person.Name", "Please specify a valid name!");

                return null;
            }

            return inputModel;
        }

        public bool SupportsModelType(Type modelType) {
            return modelType != null && modelType.IsType<PersonInputModel>();
        }

        #endregion

        private static PersonInputModel CreatePersonInput(ControllerContext controllerContext) {
            return new PersonInputModel
                   {
                       Name = controllerContext.HttpContext.Request.Form["Name"]
                   };
        }
    }
}