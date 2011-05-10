namespace MvcTurbine.Samples.ModelBinders.Models {
    public class PersonValidator : IModelValidator {
        public bool IsValid<TModel>(TModel model) {
            var person = model as PersonInputModel;
            if (person != null) {
                return !string.IsNullOrEmpty(person.Name);
            }

            return false;
        }
    }
}