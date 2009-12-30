namespace MvcTurbine.Samples.ModelBinders.Registration {
    using ComponentModel;
    using Models;

    public class ValidationRegistration : IServiceRegistration {
        #region IComponentRegistration Members

        public void Register(IServiceLocator locator) {
            locator.Register<IModelValidator, PersonValidator>();
        }

        #endregion
    }
}