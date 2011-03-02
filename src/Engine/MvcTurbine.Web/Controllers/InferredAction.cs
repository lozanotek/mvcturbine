namespace MvcTurbine.Web.Controllers {
    using System;

    /// <summary>
    /// Defines the components of an inferred action.
    /// </summary>
    [Serializable]
    public class InferredAction {
        /// <summary>
        /// Gets or sets the type of the controller to check against.
        /// </summary>
        public Type Controller { get; set; }

        /// <summary>
        /// Gets or sets the name of the action to check against.
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the model provider to use for the action.
        /// </summary>
        public Func<object> ModelProvider { get; set; }
    }
}