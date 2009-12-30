namespace MvcTurbine.Samples.ModelBinders.Models {
    public interface IModelValidator
    {
        bool IsValid<TModel>(TModel model);
    }
}