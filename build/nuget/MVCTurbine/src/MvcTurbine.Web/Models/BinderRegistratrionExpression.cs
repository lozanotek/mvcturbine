namespace MvcTurbine.Web.Models {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ComponentModel;

    ///<summary>
    /// Expression for the registration of <see cref="IModelBinder"/> within the system.
    ///</summary>
    public class BinderRegistratrionExpression {
        protected TypeCache BinderTable { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="cache"></param>
        public BinderRegistratrionExpression(TypeCache cache) {
            BinderTable = cache ?? new TypeCache();
        }

        /// <summary>
        /// Registers the specified model with the specified binder.
        /// </summary>
        /// <typeparam name="TModel">Type of model to register.</typeparam>
        /// <typeparam name="TBinder">Type of binder to register.</typeparam>
        /// <returns></returns>
        public BinderRegistratrionExpression Bind<TModel, TBinder>()
            where TBinder : IModelBinder {

            return Bind(typeof(TModel), typeof(TBinder));
        }

        /// <summary>
        /// Registers the specified model with the specified binder.
        /// </summary>
        /// <param name="modelType">Type of model to register.</param>
        /// <param name="binderType">Type of binder to register.</param>
        /// <returns></returns>
        public BinderRegistratrionExpression Bind(Type modelType, Type binderType) {
            BinderTable.Add(new KeyValuePair<Type, Type>(modelType, binderType));
            return this;
        }
    }
}
