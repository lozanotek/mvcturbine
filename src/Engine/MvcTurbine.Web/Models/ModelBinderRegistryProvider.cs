namespace MvcTurbine.Web.Models {
    using System;
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Provides resolution of <see cref="IModelBinder"/> using the types from <see cref="ModelBinderRegistry"/>.
    /// </summary>
    public class ModelBinderRegistryProvider : IModelBinderProvider {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serviceLocator"></param>
        /// <param name="cache"></param>
        public ModelBinderRegistryProvider(IServiceLocator serviceLocator, TypeCache cache) {
            ServiceLocator = serviceLocator;
            BinderCache = cache;
        }

        /// <summary>
        /// Gets the associated <see cref="IServiceLocator"/> with the provider.
        /// </summary>
        public IServiceLocator ServiceLocator { get; set; }

        /// <summary>
        /// Gets the cache associated with the provider.
        /// </summary>
        public TypeCache BinderCache { get; private set; }

        /// <summary>
        /// Gets the binder based on the specified model.
        /// </summary>
        /// <param name="modelType">Type of model to use.</param>
        /// <returns></returns>
        public IModelBinder GetBinder(Type modelType) {
            if (BinderCache == null) return null;
            if (!BinderCache.ContainsKey(modelType)) return null;

            var binderType = BinderCache[modelType];
            return ServiceLocator.Resolve(binderType) as IModelBinder;
        }
    }
}
