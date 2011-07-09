namespace MvcTurbine.Web.Models {
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Defines a common interface for checking whether a <see cref="IModelBinder"/> should
    /// be applied to a specified model.
    /// </summary>
    [Obsolete("This method of registering IModelBinders will be retired in favor of the ModelBinderRegistry model.", false)]
    public interface IFilterableModelBinder : IModelBinder {
        /// <summary>
        /// Checks whether the current instance supports the specified type.
        /// </summary>
        /// <param name="modelType">Type to check against.</param>
        /// <returns>True of they're the same, false otherwise.</returns>
        bool SupportsModelType(Type modelType);
    }
}
