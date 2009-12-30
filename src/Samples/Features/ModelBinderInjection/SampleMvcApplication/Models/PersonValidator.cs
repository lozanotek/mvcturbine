namespace MvcTurbine.Samples.ModelBinders.Models {
    public class PersonValidator : IModelValidator {
        #region IModelValidator Members

        public bool IsValid<TModel>(TModel model) {
            var person = model as PersonInputModel;
            if (person != null) {
                return !string.IsNullOrEmpty(person.Name);
            }

            return false;
        }

        #endregion
    }
}