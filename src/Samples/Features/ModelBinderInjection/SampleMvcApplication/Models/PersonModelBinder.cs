namespace MvcTurbine.Samples.ModelBinders.Models {
    using System;
    using System.Web.Mvc;
    using Web.Models;

    public class PersonModelBinder : IModelBinder {
        public PersonModelBinder(IModelValidator validator) {
            Validator = validator;
        }

        public IModelValidator Validator { get; private set; }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            // Creates the person from the input form values
			PersonInputModel inputModel = new PersonInputModel
			{
				Name = controllerContext.HttpContext.Request.Form["Name"]
			};

            // Use the injected model validator to check if the values are proper
            if (!Validator.IsValid(inputModel)) {
                bindingContext.ModelState.AddModelError("person.Name", "Please specify a valid name!");

                return null;
            }

            return inputModel;
        }
    }
}
