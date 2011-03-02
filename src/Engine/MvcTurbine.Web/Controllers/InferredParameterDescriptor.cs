namespace MvcTurbine.Web.Controllers {
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Wrapper class defines the type of parameter to use with the <see cref="InferredActionDescriptor"/>.
    /// </summary>
    public class InferredParameterDescriptor : ParameterDescriptor {
        private readonly ActionDescriptor descriptor;
        private readonly string parameterName;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="parameterName"></param>
        public InferredParameterDescriptor(ActionDescriptor descriptor, string parameterName) {
            this.descriptor = descriptor;
            this.parameterName = parameterName;
        }

        /// <summary>
        /// See <see cref="ParameterDescriptor.ActionDescriptor"/>.
        /// </summary>
        public override ActionDescriptor ActionDescriptor {
            get { return descriptor; }
        }

        /// <summary>
        /// Returns the name of the executing action.
        /// </summary>
        public override string ParameterName {
            get { return parameterName; }
        }

        /// <summary>
        /// Returns <see cref="string"/> as the default type.
        /// </summary>
        public override Type ParameterType {
            get { return typeof(string); }
        }
    }
}