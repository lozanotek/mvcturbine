namespace MvcTurbine.Web.Controllers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Blades;

    /// <summary>
    /// Defines a custom <see cref="ControllerActionInvoker"/> to use that will 
    /// infer actions that are not defined.
    /// </summary>
    public class TurbineActionInvoker : ControllerActionInvoker {

        ///<summary>
        /// Creates a new instance of the <see cref="ControllerActionInvoker"/> that 
        /// provides action execution for the controller
        ///</summary>
        ///<param name="locator"></param>
        public TurbineActionInvoker(IEnumerable<InferredAction> actionRegistrations) {
            ActionRegistrations = actionRegistrations;
        }

        /// <summary>
        /// Gets the associated <see cref="InferredAction"/> types with the system.
        /// </summary>
        public IEnumerable<InferredAction> ActionRegistrations { get; private set; }

        /// <summary>
        /// Finds the action for the controller, if not it is inferred.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="controllerDescriptor"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        protected override ActionDescriptor FindAction(ControllerContext controllerContext,
            ControllerDescriptor controllerDescriptor, string actionName) {

            var foundAction = base.FindAction(controllerContext, controllerDescriptor, actionName);
            if (foundAction == null) {
                InferredAction inferredAction = GetInferredAction(controllerDescriptor, actionName);
                foundAction = new InferredActionDescriptor(actionName, controllerDescriptor, inferredAction);
            }
            return foundAction;
        }

        protected virtual InferredAction GetInferredAction(ControllerDescriptor controllerDescriptor, string actionName) {
            return InferredActions.Current
                .Where(inferred => inferred.Controller == controllerDescriptor.ControllerType)
                .Where( inferred =>
                    string.Equals(inferred.ActionName, actionName, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();
        }
    }
}
