namespace MvcTurbine.Web.Filters {
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Simple class for registering controller associated filters.
    /// </summary>
    [Serializable]
    public class ControllerFilterReg : FilterReg {
        /// <summary>
        /// Gets or sets the type of the controller.
        /// </summary>
        public Type Controller { get; set; }

        /// <summary>
        /// Gets the <see cref="FilterScope"/> for this registration.
        /// </summary>
        public override FilterScope Scope {
            get { return FilterScope.Controller; }
        }
    }
}
