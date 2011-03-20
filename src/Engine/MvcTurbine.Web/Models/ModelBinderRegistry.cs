namespace MvcTurbine.Web.Models {
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Base class to provide Model (ViewModel) registration for <see cref="IModelBinder"/>.
    /// </summary>
    public abstract class ModelBinderRegistry : BinderRegistratrionExpression {
        protected ModelBinderRegistry() : base(new TypeCache()) {
        }

        /// <summary>
        /// Gets the registered <see cref="IModelBinder"/> list for the specified models.
        /// </summary>
        /// <returns></returns>
        public virtual TypeCache GetBinderRegistrations() {
            return BinderTable;
        }
    }
}
