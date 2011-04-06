namespace MvcTurbine.Web.Controllers {
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Custom implementation of <see cref="ActionDescriptor"/> to wire up inferred action execution.
    /// </summary>
    public class InferredActionDescriptor : ActionDescriptor {
        private readonly string actionName;
        private readonly ControllerDescriptor controllerDescriptor;

        /// <summary>
        /// See <see cref="ActionDescriptor"/>
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerDescriptor"></param>
        public InferredActionDescriptor(string actionName, ControllerDescriptor controllerDescriptor, InferredAction inferredAction) {
            InferredAction = inferredAction;
            this.actionName = actionName;
            this.controllerDescriptor = controllerDescriptor;
        }

        /// <summary>
        /// See <see cref="ActionDescriptor.ActionName"/>.
        /// </summary>
        public override string ActionName {
            get { return actionName; }
        }

        /// <summary>
        /// See <see cref="ActionDescriptor.ControllerDescriptor"/>.
        /// </summary>
        public override ControllerDescriptor ControllerDescriptor {
            get { return controllerDescriptor; }
        }

        /// <summary>
        /// See <see cref="InferredAction"/>.
        /// </summary>
        public InferredAction InferredAction { get; private set; }

        /// <summary>
        /// Always returns a <see cref="ViewResult"/> with the specified <see cref="ActionName"/> as the name of the view.
        /// For more information, see <see cref="ActionDescriptor.Execute"/>.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters) {
            if (InferredAction != null && InferredAction.ModelProvider != null) {
                controllerContext.Controller.ViewData.Model = InferredAction.ModelProvider();
            }

            return new InferredViewResult {
                ViewName = ActionName,
                ViewData = controllerContext.Controller.ViewData
            };
        }

        /// <summary>
        /// Gets an one item array of <see cref="InferredParameterDescriptor"/> containing the current instance.
        /// </summary>
        /// <returns></returns>
        public override ParameterDescriptor[] GetParameters() {
            return new ParameterDescriptor[] { new InferredParameterDescriptor(this, ActionName) };
        }
    }
}