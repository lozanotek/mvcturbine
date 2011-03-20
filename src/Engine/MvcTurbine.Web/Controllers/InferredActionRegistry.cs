namespace MvcTurbine.Web.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Base class to provide Model (ViewModel) registration for <see cref="IModelBinder"/>.
    /// </summary>
    public abstract class InferredActionRegistry<TController> : IInferredActionRegistry
        where TController : IController {

        protected InferredActionRegistry() {
            ActionList = new List<InferredAction>();
        }

        /// <summary>
        /// Gets and sets the list of filter registries
        /// </summary>
        protected IList<InferredAction> ActionList { get; set; }

        /// <summary>
        /// Associates the model from the container with the specified inferred action name.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public virtual InferredActionRegistry<TController> FromContainer<TModel>(string actionName)
            where TModel : class {

            var serviceLocator = ServiceLocatorManager.Current;
            return WithProvider(actionName, serviceLocator.Resolve<TModel>);
        }

        /// <summary>
        /// Associates a model instance with the specified inferred action name.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="actionName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public virtual InferredActionRegistry<TController> WithInstance<TModel>(string actionName, TModel instance) {
            return WithProvider(actionName, () => instance);
        }


        ///<summary>
        /// Associates a model provider with the specified inferred action name.
        ///</summary>
        ///<param name="actionName"></param>
        ///<param name="modelProvider"></param>
        ///<typeparam name="TModel"></typeparam>
        ///<returns></returns>
        public virtual InferredActionRegistry<TController> WithProvider<TModel>(string actionName, Func<TModel> modelProvider) {
            Func<object> wrapper = () => modelProvider();

            var actionRegistration = new InferredAction {
                ActionName = actionName,
                Controller = typeof(TController),
                ModelProvider = wrapper
            };

            ActionList.Add(actionRegistration);
            return this;
        }

        public IEnumerable<InferredAction> GetActionRegistrations() {
            return ActionList;
        }
    }
}
